using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using elchenchenvuelvecy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace elchenchenvuelvecy.Controllers
{
    public class ToyoNoToyController(ToyoNoToyDbContext context) : Controller
    {
        private readonly ToyoNoToyDbContext _context = context;

        public IActionResult EnviarSolicitud( )
        {

            ViewBag.Codigo = TempData["CodigoSolicitud"];
            ViewBag.phone   = TempData["NumeroWhatsapp"]; 
    
                return View();
         }

        // GET: Formularios/Create
        public IActionResult Create()
        {     
              return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FormularioClass Formulario)
        {
            if (ModelState.IsValid)
            {

                var newRequest = new Request
                {  
                     Id = Guid.NewGuid(),
                     Code = $"{DateTime.Now:yyyyMMdd}-SCC{new Random().Next(10000000, 999999999)}",
                     CreationDate = DateTime.Now,
                     Suggestion = "",
                     Type = 0,
                };

                 

                if (Formulario.RequestDetail.QuantityToInvert > 25000)
                {
                    newRequest.Type = 3;
                    newRequest.Suggestion = "Gestión Caja de Ahorros";
                }
                else
                {
                    newRequest.Type = 2;
                    newRequest.Suggestion  = "Gestión directa de Ampyme"; 
                
                };


                TempData["CodigoSolicitud"] = newRequest.Code;
                TempData["NumeroWhatsapp"]  = Formulario.Contact.Phone;


                Formulario.Contact.Id = Guid.NewGuid();
                Formulario.Enterprise.Id = Guid.NewGuid();
                Formulario.RequestDetail.Id = Guid.NewGuid();


                Formulario.Contact.RequestId = newRequest.Id;
                Formulario.Enterprise.RequestId = newRequest.Id;
                Formulario.RequestDetail.RequestId = newRequest.Id; 
                _context.Requests.Add(newRequest);
                _context.Contacts.Add(Formulario.Contact);
                _context.Enterprises.Add(Formulario.Enterprise);
                _context.RequestDetails.Add(Formulario.RequestDetail);
                await _context.SaveChangesAsync();

                return RedirectToAction("EnviarSolicitud","ToyoNoToy"); 
            }

            
             return View(Formulario);
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public JsonResult SendConfirmationEmail(string email)
        {
            try
            {
                var fromAddress = new MailAddress("elchenchenvuelve@outlook.com", "ElchenChenVuelve");
                var toAddress = new MailAddress(email);
                const string fromPassword = "Elchenchen507.";
                const string subject = "Confirmación de Correo";
                const string body = "Este es un correo de confirmación.";

                var smtp = new SmtpClient
                {
                    Host = "smtp-mail.outlook.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                }; 

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }

                return Json(new { success = true, message = "Correo enviado exitosamente." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


    }
}
