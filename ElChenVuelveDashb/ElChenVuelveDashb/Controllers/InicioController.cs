using Microsoft.AspNetCore.Mvc; 
using ElChenVuelveDashb.Recursos;
using ElChenVuelveDashb.Servicios.Contrato;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ElChenVuelveDashb.Controllers
{
    public class InicioController : Controller
    {
        private readonly IUsuarioService _usuarioServicio;
        private readonly ToyoNoToyContext _context;

        public InicioController(IUsuarioService usuarioServicio, ToyoNoToyContext context)
        {
            _usuarioServicio = usuarioServicio;
            _context = context;
        }

        public async Task<IActionResult> Registrarse()
        {
            
            var roles = await _context.Roles.ToListAsync();
            ViewBag.Userprofiles = new SelectList(roles, "Id", "Perfil");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrarse(Usuario modelo)
        {   

            modelo.Password = Utilidades.EncriptarClave(modelo.Password);

            Usuario usuario_creado = await _usuarioServicio.SaveUsuario(modelo);

            if (usuario_creado.UserId > 0)
                return RedirectToAction("Registrarse", "Inicio");

            ViewData["Mensaje"] = "No se pudo crear el usuario";
            return View();
        }

        public IActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(string Username, string Password)
        {
            Usuario usuario_encontrado = await _usuarioServicio.GetUsuario(Username, Utilidades.EncriptarClave(Password));

            if (usuario_encontrado == null)
            {
                ViewData["Mensaje"] = "No se encontraron coincidencias";
                return View();
            }

            List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario_encontrado.Nombre)
                };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Formularios");
        }
    }
}
