using ElChenVuelveDashb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Twilio.TwiML.Voice;

public class ArchivosController : Controller
{
    private readonly ToyoNoToyContext _context;
    private readonly string rutaServidor = "C:\\Reportes";

    public ArchivosController(ToyoNoToyContext context)
    {
        _context = context;
    }

    // GET: Archivos
    public async Task<IActionResult> SubirArchivo()
    {
        var archivos = await _context.Archivos.ToListAsync();
        return View(archivos);
    }

    // POST: Archivos/Upload
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upload(IFormFile archivo, int usuarioId, string descripcion)
    {
        if (archivo != null && archivo.Length > 0 && archivo.Length <= 5 * 1024 * 1024)
        {
            string rutaDocumento = Path.Combine(rutaServidor, archivo.FileName);

            using (var stream = new FileStream(rutaDocumento, FileMode.Create))
            {
                await archivo.CopyToAsync(stream);
            }

            var nuevoArchivo = new Archivo
            {
                Id = usuarioId,
                Descripcion = descripcion,
                Ruta = rutaDocumento
            };

            _context.Archivos.Add(nuevoArchivo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ModelState.AddModelError("", "El archivo debe ser menor a 5 MB.");
        return View("Index", await _context.Archivos.ToListAsync());
    }
}