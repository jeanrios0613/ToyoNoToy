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

        public IActionResult DescargarReporte()//DateTime? Fecin, DateTime? Fecfin  )
        {
            
            var formularios = _context.ConsultaTodo
                                      //.Where(f => !Fecin.HasValue || f.Created >= Fecin)
                                      //.Where(f => !Fecfin.HasValue || f.Created <= Fecfin) 
                                      .ToList();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Reporte");

                // Agregar encabezados
                worksheet.Cell(1, 1).Value = "Codigodesolicitud";
                worksheet.Cell(1, 1).Value = "FechadeCreacion";
                worksheet.Cell(1, 1).Value = "FechaActualizacion";
                worksheet.Cell(1, 1).Value = "Gestor";
                worksheet.Cell(1, 1).Value = "EtapadelNegocio";
                worksheet.Cell(1, 1).Value = "CorreoElectronico";
                worksheet.Cell(1, 1).Value = "Nombre";
                worksheet.Cell(1, 1).Value = "Apellido";
                worksheet.Cell(1, 1).Value = "Numeroidentificacion";
                worksheet.Cell(1, 1).Value = "Tipoidentificacion";
                worksheet.Cell(1, 1).Value = "Telefono";
                worksheet.Cell(1, 1).Value = "NombreNegocio";
                worksheet.Cell(1, 1).Value = "Descripcionnegocio";
                worksheet.Cell(1, 1).Value = "Actividadeconomica";
                worksheet.Cell(1, 1).Value = "Instagram";
                worksheet.Cell(1, 1).Value = "RUC";
                worksheet.Cell(1, 1).Value = "WebSite";
                worksheet.Cell(1, 1).Value = "Provincia";
                worksheet.Cell(1, 1).Value = "Distrito";
                worksheet.Cell(1, 1).Value = "corregimiento";
                worksheet.Cell(1, 1).Value = "Proyeccionventasmensuales";
                worksheet.Cell(1, 1).Value = "Ventasmensuales";
                worksheet.Cell(1, 1).Value = "FechaInicioOperaciones";
                worksheet.Cell(1, 1).Value = "CuantoChenchennecesitas";
                worksheet.Cell(1, 1).Value = "Enqueloinvertiras";
                worksheet.Cell(1, 1).Value = "VerificacionCliente";
                worksheet.Cell(1, 1).Value = "GestionRealizada";
                worksheet.Cell(1, 1).Value = "Tipoatencion";
                worksheet.Cell(1, 1).Value = "Porquenocontacto";
                worksheet.Cell(1, 1).Value = "Etapa";
                worksheet.Cell(1, 1).Value = "UsuarioAsignado"; 


                // Agregar datos
                for (int i = 0; i < formularios.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = formularios[i].Codigodesolicitud;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].FechadeCreacion;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].FechaActualizacion;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].Gestor;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].EtapadelNegocio;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].CorreoElectronico;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].Nombre;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].Apellido;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].Numeroidentificacion;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].Tipoidentificacion;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].Telefono;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].NombreNegocio;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].Descripcionnegocio;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].Actividadeconomica;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].Instagram;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].RUC;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].WebSite;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].Provincia;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].Distrito;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].corregimiento;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].Proyeccionventasmensuales;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].Ventasmensuales;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].FechaInicioOperaciones;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].CuantoChenchennecesitas;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].Enqueloinvertiras;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].VerificacionCliente;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].GestionRealizada;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].Tipoatencion;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].Porquenocontacto;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].Etapa;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].UsuarioAsignado; 

                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
7                    stream.Position = 0; // Asegurarse de reiniciar la posición del stream
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
        public ActionResult Index(int id, [Bind("Id,Nombre,Apellido,Email,Cedula,Celular,RedesSociales,NombreEmpresa,ActividadEconomica,DescripcionNegocio,MontoInversion,DescripcionInversion,RazonCambio,IndicaSolicitud,DocumentacionAdjunta,FechaRegistro,UsuarioAnalista,FechaAtencion,UsuarioSupervisor,FechaAprobacion,Localidad")] Formulario formulario)
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
