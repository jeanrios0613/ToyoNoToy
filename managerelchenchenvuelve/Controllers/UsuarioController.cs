using System.Diagnostics;
using managerelchenchenvuelve.Models;
using managerelchenchenvuelve.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using managerelchenchenvuelve.Recursos;
using Microsoft.AspNetCore.Identity;

namespace managerelchenchenvuelve.Controllers
{   
    public class UsuarioController : Controller
    {
        private readonly ToyoNoToyContext _context;
        private readonly DatabaseConnection _db; 
        private readonly ILogger<UsuarioController> _logger;


        public UsuarioController(ToyoNoToyContext context, DatabaseConnection db, ILogger<UsuarioController> logger)
        {
            _context = context;
            _logger = logger;
            _db = db;
        }

       
        public IActionResult Index()
        {
            var username = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(username))
            {
                _logger.LogWarning("No se encontró usuario en la sesión");
                return RedirectToAction("Login", "Account");
            }

            var usuarios = _context.Users.ToList();
            _logger.LogInformation("Usuarios encontrados: {Count}", usuarios.Count);
              
            return View(usuarios);
        }

        // GET: Usuario/Create
        public IActionResult Create()   
        {
            var roles = _context.Roles.ToList();
            ViewBag.Roles = roles;
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Useres user, string? RoleId)
        {
            _logger.LogInformation("Iniciando creación de usuario: {RoleId}",  RoleId);      
            _logger.LogInformation("Datos recibidos - UserName: {UserName}, Email: {Email}, Names: {Names}, Lastname: {Lastname}",
                         user.UserName, user.Email, user.Names, user.Lastname);

            if (user == null)
            {
                _logger.LogWarning("El modelo de usuario recibido es null");
                ModelState.AddModelError("", "No se recibieron datos del formulario");
                return View();
            }

            if (ModelState.IsValid)
            {
                try
                {
                     

                    user.Id = Guid.NewGuid().ToString();
                    user.Created = DateTime.Now;
                    user.PasswordHash = EncryptPass.Encriptar(user.PasswordHash);
                    user.Status = true;


                    var Roles = new UserRole
                    {   UserId = user.Id,
                        RoleId = RoleId
                    };

                    _context.Users.Add(user);
                    _context.UserRoles.Add(Roles);

                    await _context.SaveChangesAsync();

                    _logger.LogInformation("Usuario creado exitosamente: {UserName}", user.UserName);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al crear usuario: {Message}", ex.Message);
                    ModelState.AddModelError("", $"Error al crear el usuario: {ex.Message}"); 
                    return RedirectToAction("ErrorSis", "Process");
                }
            }
            else
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogWarning("Error de validación: {ErrorMessage}", error.ErrorMessage);
                }
            }

            var roles = _context.Roles.ToList();
            ViewBag.Roles = roles;
            return View(user);
        }

        // GET: Usuario/UpdateUser/{id}
        public IActionResult UpdateUser(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();

            var roles = _context.Roles.ToList();
            ViewBag.Roles = roles;
            var userRole = _context.UserRoles.FirstOrDefault(ur => ur.UserId == id);
            ViewBag.CurrentRoleId = userRole?.RoleId;
            return View(user);
        }

        // POST: Usuario/UpdateUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUser(Useres user, string RoleId)
        {
            if (user == null || string.IsNullOrEmpty(user.Id))
                return NotFound();

            var userDb = _context.Users.FirstOrDefault(u => u.Id == user.Id);
            if (userDb == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                userDb.Email = user.Email;
                if (!string.IsNullOrWhiteSpace(user.PasswordHash))
                {
                    userDb.PasswordHash = EncryptPass.Encriptar(user.PasswordHash);
                }
                // Actualizar el rol
                var userRole = _context.UserRoles.FirstOrDefault(ur => ur.UserId == user.Id);
                if (userRole != null && userRole.RoleId != RoleId)
                {
                    userRole.RoleId = RoleId;
                }
                else if (userRole == null && !string.IsNullOrEmpty(RoleId))
                {
                    _context.UserRoles.Add(new UserRole { UserId = user.Id, RoleId = RoleId });
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            // Si hay error, recargar roles y rol actual
            ViewBag.Roles = _context.Roles.ToList();
            var currentRole = _context.UserRoles.FirstOrDefault(ur => ur.UserId == user.Id);
            ViewBag.CurrentRoleId = currentRole?.RoleId;
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();

            user.Status = false;
            _context.SaveChanges();

            _logger.LogInformation("Usuario {UserId} deshabilitado exitosamente", id);
            return RedirectToAction("Index");
        }

    }
}
