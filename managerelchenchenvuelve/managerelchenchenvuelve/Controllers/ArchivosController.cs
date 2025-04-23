using managerelchenchenvuelve.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ArchivosController : Controller
{
    private readonly ToyoNoToyContext _context;
    private readonly string rutaServidor = "C:\\Reportes";

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
    public IActionResult SubirArchivo(string Id)
    {

        var archivos = _context.DocumentReferences.Where(a => a.RequiredName == Id).ToList();
        // Imprimir los archivos en la consola
        Console.WriteLine("Archivos obtenidos:");
        foreach (var archivo in archivos)
        {
            Console.WriteLine($"Id: {archivo.RequiredName}, Nombre: {archivo.DocumentTitle}, Ruta: {archivo.StageName}"); // Modifica según las propiedades de tu modelo
        }

        // Pasar los archivos y el Id a la vista
        ViewBag.Id = Id;
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
                StageName = rutaDocumento
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
