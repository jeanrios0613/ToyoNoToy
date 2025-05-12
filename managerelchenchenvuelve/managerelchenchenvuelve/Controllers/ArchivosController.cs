using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using managerelchenchenvuelve.Models;
using managerelchenchenvuelve.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using DocumentFormat.OpenXml.Office2010.Excel;



public class ArchivosController : Controller
{ 
    
    private readonly ToyoNoToyContext   _context;
    private readonly DatabaseConnection _db;
    private readonly string rutaServidor = "../Reportes";


   
    public ArchivosController(ToyoNoToyContext context, DatabaseConnection db)
    {
        _context = context;
        _db      = db;
    }



    // GET: Archivos/Usuario/{Id}
    public async Task<IActionResult> VerArchivos(string Id)
    {
        var archivos = await _context.DocumentReferences
                                     .Where(a => a.RequiredName == Id)
                                     .ToListAsync();

        return View(archivos);
    }


    
    // GET: Archivos/SubirArchivo
    public IActionResult SubirArchivo(string? ProcessId)
    {   List<DatosCliente> Datos = new List<DatosCliente>();

        string query = @"SELECT * FROM [ToyNoToy].[dbo].[Request_Info] WHERE Codigo_de_solicitud = @Codigo";

        SqlParameter[] parameters = new SqlParameter[]
        {
                    new SqlParameter("@Codigo",ProcessId)
            };


        DataTable result = _db.ExecuteQuery(query, parameters);

        foreach (DataRow row in result.Rows)
        {
            Datos.Add(new DatosCliente
            {
                reference = row["Codigo_de_solicitud"].ToString(),
                name = row["Nombre"].ToString() 
            });
        }

        var archivos = _context.DocumentReferences
                               .Where(a => a.RequiredName == ProcessId)
                               .ToList();

        ViewBag.Id = ProcessId;
        ViewBag.Referencia = Datos.FirstOrDefault()?.reference ?? "No encontrado";
        return View(archivos);
    }

    // POST: Archivos/SubirArchivo
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SubirArchivo(IFormFile archivo, string Id, string descripcion)
    {
        if (archivo != null && archivo.Length > 0 && archivo.Length <= 5 * 1024 * 1024)
        {
            string rutaDocumento = Path.Combine(rutaServidor, archivo.FileName);

            using (var stream = new FileStream(rutaDocumento, FileMode.Create))
            {
                await archivo.CopyToAsync(stream);
            }

            var nuevoArchivo = new DocumentReference
            {
                Id = Guid.NewGuid(),
                ProcessInstanceId = Guid.NewGuid(),
                RequiredName = Id,
                DocumentTitle = descripcion,
                StageName = rutaDocumento,
                IsDeleted = false,
            };

            _context.DocumentReferences.Add(nuevoArchivo);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(SubirArchivo), new { ProcessId = Id });
        }

        ModelState.AddModelError("", "El archivo debe ser menor a 5 MB.");
        return View();
    }
}
