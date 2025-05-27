using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging; 
using managerelchenchenvuelve.Services;
using managerelchenchenvuelve.Recursos;
using System.Data;
using System.Security.Claims;
using Microsoft.Data.SqlClient;
using managerelchenchenvuelve.Models;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Office2016.Drawing.Charts;

namespace managerelchenchenvuelve.Controllers
{
    public class AccountController : Controller
    {
        private readonly ToyoNoToyContext _context;
        private readonly DatabaseConnection _db;
        private readonly ILogger<AccountController> _logger;

        public AccountController(ToyoNoToyContext context, DatabaseConnection db, ILogger<AccountController> logger)
        {
            _context = context;
            _db = db;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Process");
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string username, string password, string returnUrl = null)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ViewData["Mensaje"] = "Usuario y contraseña son requeridos.";
                return View();
            }

            List<VwUserRolesInfo> users = new List<VwUserRolesInfo>();


            string encryptedPassword = EncryptPass.Encriptar(password);

            string query =  " SELECT (case  " +
                            " when  UR.rolname = 'ADMINISTRADOR' " +
                            " then 'chenchen'  " +
                            " else UR.Username " +
                            " END) AS userss  , Nombre, RolName , status" +
                            " FROM [vw_UserRolesInfo] as UR " +
                            " WHERE [UserName] = @username AND [PasswordHash] = @PasswordHash";

            SqlParameter[] parameters = new SqlParameter[]
             {
                new SqlParameter("@username", username),
                new SqlParameter("@PasswordHash", encryptedPassword)
            };

            DataTable result;
           

            try
            {
                result = _db.ExecuteQuery(query, parameters);
                
                foreach (DataRow row in result.Rows)
                {
                    string? StatusUser = row["status"].ToString();

                    users.Add(new VwUserRolesInfo
                    {
                         Nombre    = row["Nombre"].ToString(),
						 RolName   = row["RolName"].ToString(), 
                         Username  = row["userss"].ToString(),
                         Status    = Convert.ToBoolean(row["status"].ToString()),
                    });
                }

              
                if (users.Count > 0)
                {
                    HttpContext.Session.SetString("Userss", users[0].Username);
                    HttpContext.Session.SetString("Nombre", users[0].Nombre);
					HttpContext.Session.SetString("Roles" , users[0].RolName);
				}

                _logger.LogInformation("Resultado de login para {Username}: {Rows} filas", username, result.Rows.Count);

                if (users.Count == 0)
                {
                    ViewData["Mensaje"] = "Usuario o contraseña incorrecta.";
                    return View();
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error consultando usuario en base de datos.");
                ViewData["Mensaje"] = "Error al conectarse con la base de datos.";
                return View();
            }

            if (users[0].Status == false )
            {
                ViewData["Mensaje"] = "Usuario Deshabilitado.";
                return View();

            }

            if (result.Rows.Count == 0)
            {
                ViewData["Mensaje"] = "Usuario o contraseña incorrecta.";
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, users[0].Nombre),
                new Claim(ClaimTypes.Role, users[0].RolName)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
            };

            try
            {
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties
                );

                _logger.LogInformation("Usuario {Username} autenticado exitosamente.", username);

                HttpContext.Session.SetString("UserName", username);
               
                // Redirección segura
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Process");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error durante el inicio de sesión del usuario: {Username}", username);
                ViewData["Mensaje"] = "Ocurrió un error durante el inicio de sesión.";
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}
