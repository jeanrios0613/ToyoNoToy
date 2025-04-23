using ElChenVuelveDashb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ElChenVuelveDashb;

public class ArchivosController : Controller
{
    private readonly ToyoNoToyContext _context;
    private readonly string rutaServidor = "C:\\Reportes";

    public ArchivosController(ToyoNoToyContext context)
    {
        _context = context;
    }

    // GET: Archivos/Usuario/{Id}
    public async Task<IActionResult> VerArchivos(int Id)
    {
        // Obtener todos los archivos del usuario específico
        var archivos = await _context.ArchivoSubidos.Where(a => a.Id == Id).ToListAsync();
        return View(archivos);
    }

    // GET: Archivos/SubirArchivo
    public IActionResult SubirArchivo(int Id)
    {
       
        var archivos = _context.ArchivoSubidos.Where(a => a.Id == Id).ToList();
        // Imprimir los archivos en la consola
        Console.WriteLine("Archivos obtenidos:");
        foreach (var archivo in archivos)
        {
            Console.WriteLine($"Id: {archivo.Id}, Nombre: {archivo.Descripcion}, Ruta: {archivo.RutaArchivo}"); // Modifica según las propiedades de tu modelo
        }

        // Pasar los archivos y el Id a la vista
        ViewBag.Id = Id;
        return View(archivos);
    }

    // POST: Archivos/SubirArchivo
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SubirArchivo(IFormFile archivo, int Id, string descripcion)
    {
        if (archivo != null && archivo.Length > 0 && archivo.Length <= 5 * 1024 * 1024)
        {
            string rutaDocumento = Path.Combine(rutaServidor, archivo.FileName);

            using (var stream = new FileStream(rutaDocumento, FileMode.Create))
            {
                await archivo.CopyToAsync(stream);
            }

            var nuevoArchivo = new ArchivoSubido
            {
                Id = Id,
                Descripcion = descripcion,
                RutaArchivo = rutaDocumento
            };

            _context.ArchivoSubidos.Add(nuevoArchivo);
            await _context.SaveChangesAsync();

            // Redirigir al usuario para que vea los archivos subidos
            return RedirectToAction(nameof(SubirArchivo), new { Id = Id });
        }

        ModelState.AddModelError("", "El archivo debe ser menor a 5 MB.");
        return View();
    }
}
