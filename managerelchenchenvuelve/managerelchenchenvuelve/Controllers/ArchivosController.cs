using System.Collections.Generic;
using System.Configuration;
using managerelchenchenvuelve.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

public class ArchivosController : Controller
{
    private readonly ToyoNoToyContext _context;
    private readonly string rutaServidor = "../Reportes";
    private readonly string _connectionString = "server=TSIAPP724; database=PoluxDb; integrated security=true;TrustServerCertificate=True;";



    public ArchivosController(ToyoNoToyContext context)
    {
        _context = context; 
    }

    // GET: Archivos/Usuario/{Id}
    public async Task<IActionResult> VerArchivos(string Id)
    {
       

        // Obtener todos los archivos del usuario específico
        var archivos = await _context.DocumentReferences.Where(a => a.RequiredName == Id).ToListAsync();
        return View(archivos);
    }

    // GET: Archivos/SubirArchivo
    public IActionResult SubirArchivo(string? ProcessId)
    {
        List<DatosCliente> Datos = new List<DatosCliente>();

        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM [Consulta_solo_ampyme_completo] WHERE [Codigo De Solicitud] = @ProcessId";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ProcessId", ProcessId);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Datos.Add(new DatosCliente
                {
                    reference = reader["Id"].ToString(),
                    name = reader["Nombre"].ToString()
                });
            }
            reader.Close();
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

            // Redirigir al usuario para que vea los archivos subidos
            return RedirectToAction(nameof(SubirArchivo), new { Id = Id });
        }

        ModelState.AddModelError("", "El archivo debe ser menor a 5 MB.");
        return View();
    }
}
