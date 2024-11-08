using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ElChenVuelve.Models;

namespace ElChenVuelve.Controllers
{
    public class FormulariosController : Controller
    {
        private readonly ToyoNoToyContext _context;

        public FormulariosController(ToyoNoToyContext context)
        {
            _context = context;
        }


        // GET: Formularios/Create
        public async Task<IActionResult> Create()
        {
            // Obtener la lista de roles de la base de datos
            var Items = await _context.Actividads.ToListAsync();

            // Pasar los datos de Id y Perfil a ViewBag como SelectList
            ViewBag.Items = new SelectList(Items, "Id", "NombreActividad");

            return View();
        }



        // POST: Formularios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,Email,Cedula,Celular,RedesSociales,NombreEmpresa,ActividadEconomica,DescripcionNegocio,MontoInversion,DescripcionInversion,RazonCambio,IndicaSolicitud,DocumentacionAdjunta,FechaRegistro,UsuarioAnalista,FechaAtencion,UsuarioSupervisor,FechaAprobacion,Localidad")] Formulario formulario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(formulario);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Su solicitud fue enviada con éxito";
                return RedirectToAction(nameof(Create));
            }
            return View(formulario);
        }

      
        private bool FormularioExists(int id)
        {
            return _context.Formularios.Any(e => e.Id == id);
        }
    }
}
