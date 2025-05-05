using System.Drawing.Printing;
using System.Security.Claims;
using BootstrapBlazor.Components;
using managerelchenchenvuelve.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace managerelchenchenvuelve.Controllers
{
   //[Authorize]
    public class ProcessController : Controller
    {
        private readonly ToyoNoToyContext _context;
        private readonly ILogger<ProcessController> _logger;

        public ProcessController(ToyoNoToyContext context, ILogger<ProcessController> logger)
        {
            _context = context;   
            _logger = logger;

        }

        // GET: ProcessController
        public ActionResult Index(string? id = null, int page = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Accediendo a Process/Index");

                var username = HttpContext.Session.GetString("UserName"); // Obtener el nombre de usuario de la sesión
                _logger.LogInformation("Usuario en sesión: {Username}", username);

                if (string.IsNullOrEmpty(username))
                {
                    _logger.LogWarning("No se encontró usuario en la sesión");
                    return RedirectToAction("Login", "Account");
                }

                ViewData["nombreUsuario"] = username;
                

                if (id == null)
                {
                    _logger.LogInformation("Obteniendo lista de formularios");

                    var formularios = _context.ConsultaSoloAmpymeCompletos 
                        .Skip((page - 1) * pageSize)  
                        .Take(pageSize)  
                        .ToList();

                    var totalCount = _context.ConsultaSoloAmpymeCompletos.Count();  
                    ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
                    ViewBag.CurrentPage = page;

                    return View(formularios);  
                }
                else
                {
                    _logger.LogInformation("Obteniendo formulario específico: {Id}", id);
                    var formulario = _context.ConsultaSoloAmpymeCompletos 
                        .Where(f => id == null || f.CodigoDeSolicitud == id)  
                        .FirstOrDefault();  

                    return View(formulario);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en Process/Index");
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: ProcessController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProcessController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProcessController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProcessController/solicitud
        public ActionResult solicitud(string? id)
        {    
            if (id == null)
            {
                return NotFound();
            }

			var formulario = _context.ConsultaSoloAmpymeCompletos
					.Where(f => f.CodigoDeSolicitud == id)
					.FirstOrDefault();
			if (formulario == null)
            {
                return NotFound();
            }

            // Obtener los archivos asociados al formulario
            //var archivos = await _context.DocumentReferences.Where(a => a.DocumentHandle == id).ToListAsync();

            // Pasar los datos del formulario y los archivos a la vista
            //ViewBag.Archivos = archivos;

            ViewBag.Codigo = formulario.CodigoDeSolicitud;

            return View(formulario);
        }

        // GET: ProcessController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProcessController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProcessController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProcessController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
