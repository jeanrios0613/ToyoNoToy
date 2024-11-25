using ElChenVuelveDashb.Models;
using Microsoft.AspNetCore.Mvc;

namespace ElChenVuelveDashb.Controllers
{
    public class ForArViewModelController : Controller
    {
        private readonly ToyoNoToyContext _context;

        public ForArViewModelController(ToyoNoToyContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var formulario = _context.Formularios.FirstOrDefault(); // Obtener datos de formulario
            var archivo = new Archivo(); // Crear un nuevo archivo, o buscar uno existente si es necesario

            var viewModel = new ForArViewModel
            {
                Formulario = formulario,
                Archivo = archivo
            };

            return View(viewModel);
        }
    }
}
