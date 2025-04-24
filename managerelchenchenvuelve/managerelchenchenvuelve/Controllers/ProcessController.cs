using System.Drawing.Printing;
using System.Security.Claims;
using BootstrapBlazor.Components;
using managerelchenchenvuelve.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace managerelchenchenvuelve.Controllers
{
    public class ProcessController : Controller
    { private readonly ToyoNoToyContext _context;

        public ProcessController(ToyoNoToyContext context)
        {
            _context = context;   
        }

        // GET: ProcessController
        public ActionResult Index(string? id = null, int page = 1, int pageSize = 10 )
        { 
            ClaimsPrincipal claimuser = HttpContext.User;
            string? nombreUsuario = null; 

            if (claimuser.Identity.IsAuthenticated)
            {
                nombreUsuario = claimuser.Claims.Where(c => c.Type == ClaimTypes.Name)
                                                .Select(c => c.Value).SingleOrDefault();

            }

            ViewData["nombreUsuario"] = nombreUsuario;

             
            if (id == null )
            { 
                var formularios = _context.ConsultaSoloAmpymeCompletos 
                                //.OrderBy(f => f.FechaDeActualizacion)  Ordenar por Id
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
                 
                var formulario = _context.ConsultaSoloAmpymeCompletos 
                    .Where(f => id == null || f.CodigoDeSolicitud == id)  
                    .FirstOrDefault();  




                if (formulario == null)
                {
                    return NotFound();  
                }

                return View(formulario); 
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
