using ElChenVuelveDashb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ElChenVuelveDashb.Controllers
{
    public class ForArViewModelController : Controller
    {
        private readonly ToyoNoToyContext _context;

        public ForArViewModelController(ToyoNoToyContext context)
        {
            _context = context;
        }

        public IActionResult Index(int id)
        {
            // Buscar formulario por ID
            var formulario = _context.Formularios.FirstOrDefault(f => f.Id == id);

            // Buscar archivo asociado por ID (si aplicable) o ajustarlo a tus necesidades
            var archivo = _context.ArchivoSubidos.FirstOrDefault(a => a.Id == id);

            // Validar si no se encuentra el formulario
            if (formulario == null)
            {
                return NotFound($"No se encontró un formulario con el ID {id}");
            }

            // Crear el ViewModel con los datos encontrados
            var viewModel = new ForArViewModel
            {
                Formulario = formulario,
                Archivo = archivo
            };

            return View(viewModel);
        }
    }
}
