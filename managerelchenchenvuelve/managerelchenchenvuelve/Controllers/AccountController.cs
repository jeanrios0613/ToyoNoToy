using Microsoft.AspNetCore.Mvc;
using managerelchenchenvuelve.Models.Interface;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; 
using managerelchenchenvuelve.Services;
using managerelchenchenvuelve.Models;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.Data; 
using managerelchenchenvuelve.Recursos;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace managerelchenchenvuelve.Controllers
{
    public class AccountController : Controller
    { 
        private readonly IdentityServerContext _context; 
        private readonly DatabaseServerAdmin _db;

        public AccountController(IdentityServerContext context, DatabaseServerAdmin db)
        { 
            _context = context;
            _db = db;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string username, string password, string returnUrl = null)
        {
            String passw = EncryptPass.Encriptar(password);

            List<DatosCliente> Datos = new List<DatosCliente>();

            string query = "SELECT * FROM [Users] WHERE [UserName] = @username AND [PasswordHash] = @PasswordHash";

            SqlParameter[] param = { new SqlParameter("@username", username ?? (object)DBNull.Value),
                                          new SqlParameter("@PasswordHash", passw ?? (object)DBNull.Value)
                                          }; 

            DataTable result = _db.ExecuteQuery(query, param);

            if (result.Rows.Count == 0) {
                ViewData["Mensaje"] = "Usuario o Contraseña Incorrecta";
                return View(); 
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "User")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Process");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
} 