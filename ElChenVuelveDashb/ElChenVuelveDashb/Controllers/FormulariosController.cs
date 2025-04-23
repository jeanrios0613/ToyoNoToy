using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ElChenVuelveDashb.Models;
using System.Security.Claims;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;

namespace ElChenVuelveDashb.Controllers
{ 
    public class FormulariosController : Controller
    {
        private readonly ToyoNoToyContext _context;
        private readonly string rutaServidor = "C:\\Reportes";

        public FormulariosController(ToyoNoToyContext context)
        {
            _context = context;
        }

        public ActionResult Reporte()
        {
            return View();
        }

        public IActionResult DescargarReporte(DateTime? Fecin, DateTime? Fecfin  )
        {
            
            var formularios = _context.ProcessInstances
                                      .Where(f => !Fecin.HasValue || f.Created >= Fecin)
                                      .Where(f => !Fecfin.HasValue || f.Created <= Fecfin) 
                                      .ToList();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Reporte");

                // Agregar encabezados
                worksheet.Cell(1, 1).Value = "Nombre";
                worksheet.Cell(1, 2).Value = "Apellido";
                worksheet.Cell(1, 3).Value = "Email";
                worksheet.Cell(1, 4).Value = "Cedula";
                worksheet.Cell(1, 5).Value = "Celular";
                worksheet.Cell(1, 6).Value = "Redes Sociales";
                worksheet.Cell(1, 7).Value = "Nombre Empresa";
                worksheet.Cell(1, 8).Value = "Actividad Economica";
                worksheet.Cell(1, 9).Value = "Descripcion Negocio";
                worksheet.Cell(1, 10).Value = "Monto Inversion";
                worksheet.Cell(1, 11).Value = "Descripcion Inversion";
                worksheet.Cell(1, 12).Value = "Razon Cambio";
                worksheet.Cell(1, 13).Value = "Indica Solicitud";
                worksheet.Cell(1, 14).Value = "Fecha Registro";  // Ajustar la columna que faltaba
                worksheet.Cell(1, 15).Value = "Usuario Analista";
                worksheet.Cell(1, 16).Value = "Fecha Atencion";
                worksheet.Cell(1, 17).Value = "Usuario Supervisor";
                worksheet.Cell(1, 18).Value = "Fecha Aprobacion";
                worksheet.Cell(1, 19).Value = "Localidad";

                // Agregar datos
                /*for (int i = 0; i < formularios.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = formularios[i].Nombre;
                    worksheet.Cell(i + 2, 2).Value = formularios[i].Apellido;
                    worksheet.Cell(i + 2, 3).Value = formularios[i].Email;
                    worksheet.Cell(i + 2, 4).Value = formularios[i].Cedula;
                    worksheet.Cell(i + 2, 5).Value = formularios[i].Celular;
                    worksheet.Cell(i + 2, 6).Value = formularios[i].RedesSociales;
                    worksheet.Cell(i + 2, 7).Value = formularios[i].NombreEmpresa;
                    worksheet.Cell(i + 2, 8).Value = formularios[i].ActividadEconomica;
                    worksheet.Cell(i + 2, 9).Value = formularios[i].DescripcionNegocio;
                    worksheet.Cell(i + 2, 10).Value = formularios[i].MontoInversion;
                    worksheet.Cell(i + 2, 11).Value = formularios[i].DescripcionInversion;
                    worksheet.Cell(i + 2, 12).Value = formularios[i].RazonCambio;
                    worksheet.Cell(i + 2, 13).Value = formularios[i].IndicaSolicitud;
                    worksheet.Cell(i + 2, 14).Value = formularios[i].FechaRegistro;  // Columna corregida
                    worksheet.Cell(i + 2, 15).Value = formularios[i].UsuarioAnalista;
                    worksheet.Cell(i + 2, 16).Value = formularios[i].FechaAtencion;
                    worksheet.Cell(i + 2, 17).Value = formularios[i].UsuarioSupervisor;
                    worksheet.Cell(i + 2, 18).Value = formularios[i].FechaAprobacion;
                    worksheet.Cell(i + 2, 19).Value = formularios[i].Localidad;
                }*/

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Position = 0; // Asegurarse de reiniciar la posición del stream
                    var fileName = "Reporte.xlsx";
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
            }
        }



        // GET: Formularios
        public ActionResult Index(String? id,  int page = 1, int pageSize = 10)
        {
            ClaimsPrincipal claimuser = HttpContext.User;
            string? nombreUsuario = null;

            if (claimuser.Identity.IsAuthenticated)
            {
                nombreUsuario = claimuser.Claims.Where(c => c.Type == ClaimTypes.Name)
                                                .Select(c => c.Value).SingleOrDefault();

            }

            ViewData["nombreUsuario"] = nombreUsuario;


            // Si id y NIC son nulos, se obtienen todos los formularios con paginación
            if (id == null )
            {
                var formularios = _context.ProcessInstances
                    .OrderBy(f => f.Id) // Ordenar por Id
                    .Skip((page - 1) * pageSize) // Paginación: omitir elementos
                    .Take(pageSize) // Tomar el número especificado de elementos
                    .ToList();

                var totalCount = _context.ProcessInstances.Count(); // Contar total de formularios
                ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
                ViewBag.CurrentPage = page;

                return View(formularios); // Retorna la lista de formularios
            }
            else
            {
                // Si se proporciona un id, se busca un formulario específico
                var formulario = _context.ProcessInstances
                    //.Where(f => NIC == null || f.Cedula == NIC) // Filtrar por NIC si no es nulo
                    .Where(f => id == null || f.TransformedSequential == id) // Filtrar por id si no es nulo
                    .FirstOrDefault(); // Obtener el primer resultado o nulo si no hay coincidencias




                if (formulario == null)
                {
                    return NotFound(); // Manejo de error si no se encuentra el formulario
                }

                return View(formulario); // Retorna el formulario específico
            }
        }


        // POST: Formularios
        [HttpPost("{Id:int}")]
        [ValidateAntiForgeryToken]
       /* public ActionResult Index(int id, [Bind("Id,Nombre,Apellido,Email,Cedula,Celular,RedesSociales,NombreEmpresa,ActividadEconomica,DescripcionNegocio,MontoInversion,DescripcionInversion,RazonCambio,IndicaSolicitud,DocumentacionAdjunta,FechaRegistro,UsuarioAnalista,FechaAtencion,UsuarioSupervisor,FechaAprobacion,Localidad")] Formulario formulario)
        {
            if (!ModelState.IsValid)
            {
                // Si el modelo no es válido, devuelve la lista de formularios para volver a mostrar el formulario
                var formularios = _context.ProcessInstances.ToList();
                return View(formularios);
            }

            // Encuentra el formulario existente en la base de datos
            var formularioExistente = _context.ProcessInstances.Find(id);
            if (formularioExistente == null)
            {
                return NotFound(); // Manejo de error si no se encuentra el formulario para actualizar
            }

            // Actualiza los valores del formulario existente
            formularioExistente.Nombre    = formulario.Nombre;
            formularioExistente.Apellido = formulario.Apellido;
            formularioExistente.Email = formulario.Email;
            formularioExistente.Cedula = formulario.Cedula;
            formularioExistente.Celular = formulario.Celular;
            formularioExistente.RedesSociales = formulario.RedesSociales;
            formularioExistente.NombreEmpresa = formulario.NombreEmpresa;
            formularioExistente.ActividadEconomica = formulario.ActividadEconomica;
            formularioExistente.DescripcionNegocio = formulario.DescripcionNegocio;
            formularioExistente.MontoInversion = formulario.MontoInversion;
            formularioExistente.DescripcionInversion = formulario.DescripcionInversion;
            formularioExistente.RazonCambio = formulario.RazonCambio;
            formularioExistente.IndicaSolicitud = formulario.IndicaSolicitud;
            formularioExistente.DocumentacionAdjunta = formulario.DocumentacionAdjunta;
            formularioExistente.FechaRegistro = formulario.FechaRegistro;
            formularioExistente.UsuarioAnalista = formulario.UsuarioAnalista;
            formularioExistente.FechaAtencion = formulario.FechaAtencion;
            formularioExistente.UsuarioSupervisor = formulario.UsuarioSupervisor;
            formularioExistente.FechaAprobacion = formulario.FechaAprobacion;
            formularioExistente.Localidad = formulario.Localidad;

            // Guarda los cambios en la base de datos
            _context.SaveChanges();

            // Redirige a la vista con el formulario actualizado
            return RedirectToAction(nameof(Index), new { id = formulario.Id });
        }
        */



        // GET: Formularios/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formulario = await _context.ProcessInstances
                .FirstOrDefaultAsync(m => m.TransformedSequential == id);
            if (formulario == null)
            {
                return NotFound();
            }

            return View(formulario);
        }

        // GET: Formularios/Create
        public IActionResult Create()
        {
            return View();
        }

          
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProcessInstance formulario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(formulario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(formulario);
        }


        // GET: Formularios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formulario = await _context.ProcessInstances.FindAsync(id);
            if (formulario == null)
            {
                return NotFound();
            }

            // Obtener los archivos asociados al formulario
            var archivos = await _context.DocumentReferences.Where(a => a.DocumentHandle == id).ToListAsync();

            // Pasar los datos del formulario y los archivos a la vista
            ViewBag.Archivos = archivos;

            return View(formulario);
        }



        // POST: Formularios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,ProcessInstance formulario, IFormFile archivo, string descripcionArchivo)
        {
            if (id != formulario.Id)
            { 
                if (ModelState.IsValid)
                {
                    try
                    {
                        // Actualizar los datos del formulario
                        _context.Update(formulario);
                        await _context.SaveChangesAsync();

                        // Subir el archivo si se proporcionó uno
                        if (archivo != null && archivo.Length > 0)
                        {
                            // Ruta donde se guardará el archivo
                            string rutaDocumento = Path.Combine(rutaServidor, archivo.FileName);

                            // Guardar el archivo en el servidor
                            using (var stream = new FileStream(rutaDocumento, FileMode.Create))
                            {
                                await archivo.CopyToAsync(stream);
                            }

                            // Crear un nuevo registro de archivo en la base de datos
                            var nuevoArchivo = new DocumentReference
                            {
                                DocumentHandle = id,    
                                DocumentTitle = descripcionArchivo,
                                StageName = rutaDocumento
                            };

                            _context.DocumentReferences.Add(nuevoArchivo);
                            await _context.SaveChangesAsync();
                        }
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!FormularioExists(formulario.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(formulario);
            }

            return NotFound();
        }


        // GET: Formularios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formulario = await _context.ProcessInstances
                .FirstOrDefaultAsync(m => m.Id == id);
            if (formulario == null)
            {
                return NotFound();
            }

            return View(formulario);
        }

        // POST: Formularios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var formulario = await _context.ProcessInstances.FindAsync(id);
            if (formulario != null)
            {
                _context.ProcessInstances.Remove(formulario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormularioExists(int id)
        {
            return _context.ProcessInstances.Any(e => e.Id == id);
        }


        
    }
}
