using System.Diagnostics;
using managerelchenchenvuelve.Models;
using managerelchenchenvuelve.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace managerelchenchenvuelve.Controllers
{   
    public class UsuarioController : Controller
    {
        private readonly ToyoNoToyContext _context;
        private readonly DatabaseConnection _db; 
        private readonly ILogger<ProcessController> _logger;


        public UsuarioController(ToyoNoToyContext context, DatabaseConnection db, ILogger<ProcessController> logger)
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
    }
}
