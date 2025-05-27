using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using FormChenchen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; 
using System.ComponentModel.DataAnnotations;
using System.Globalization; 
using Microsoft.Extensions.Logging;

namespace elchenchenvuelvecy.Controllers
{
    public class ToyoNoToyController : Controller
    {
        private readonly ToyoNoToyContext _context;
        private readonly ILogger<ToyoNoToyController> _logger;

        public ToyoNoToyController(ToyoNoToyContext context, ILogger<ToyoNoToyController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult EnviarSolicitud()
        {
            _logger.LogInformation("EnviarSolicitud action called");
            _logger.LogInformation("TempData Codigo: {Codigo}", TempData["CodigoSolicitud"]);
            _logger.LogInformation("TempData Phone: {Phone}", TempData["NumeroWhatsapp"]);

            ViewBag.Codigo = TempData["CodigoSolicitud"];
            ViewBag.phone = TempData["NumeroWhatsapp"];

            return View();
        }

        // GET: Formularios/Create
        public IActionResult Create()
        {
            _logger.LogInformation("Create GET action called");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FormularioClass Formulario, string TipoFormulario, decimal MonthlySales)
        {
            string Cliente = Formulario.Contact.FullName;
            string monto = Convert.ToInt32(Formulario.Enterprise.MonthlySales).ToString();


            _logger.LogInformation("Create POST action called");
            _logger.LogInformation("Formulario data received - Contact: {@MonthlySales}", MonthlySales); 
            _logger.LogInformation("validando DAtos para MonthlySales: {@monto}", monto); 


            try
            {
                _logger.LogInformation("********************************* Insertando datos **************************************");


                //******** SE LE ESTA ASIGNANDO VALORES AL A TABLA REQUESTS ********//
                var newRequest = new Request
                {
                    Id = Guid.NewGuid(),
                    Code = $"{DateTime.Now:yyyyMMdd}-SCC{new Random().Next(10000000, 999999999)}",
                    CreationDate = DateTime.Now,
                    Suggestion = "",
                    Type = 0,
                };

                _logger.LogInformation("New request created with Code: {Code}", newRequest.Code);

                if (Formulario.RequestDetail.QuantityToInvert > 25000)
                {
                    newRequest.Type = 2;
                    newRequest.Suggestion = "Gestión Caja de Ahorros";
                    _logger.LogInformation("Request type set to 3 - Gestion Caja de Ahorros");
                }
                else
                {
                    newRequest.Type = 1;
                    newRequest.Suggestion = "Gestión directa de Ampyme";
                    _logger.LogInformation("Request type set to 2 - Gestion directa de Ampyme");
                }

                //*****************************************************************//
                //-----------------------------------------------------------------//


                //VALORES GLOBAL  PARA LA ACTION EnviarSolicitud
                TempData["CodigoSolicitud"] = newRequest.Code;
                TempData["NumeroWhatsapp"] = Formulario.Contact.Phone;



                // SE CREAN NUMERO DE FORMATO GUID PARA LAS TABLAS QUE LA NECESITAN PARA SU iD
                Formulario.Contact.Id = Guid.NewGuid();
                Formulario.Enterprise.Id = Guid.NewGuid();
                Formulario.RequestDetail.Id = Guid.NewGuid();


                //EL ID NECESARIO PARA TODAS LAS TABLAS PUEDAN HACER JOIN
                Formulario.Contact.RequestId = newRequest.Id;
                Formulario.Enterprise.RequestId = newRequest.Id;
                Formulario.RequestDetail.RequestId = newRequest.Id;


                //SE INSERTAN LOS DATOS EN LA TABLA REQUEST_INFO PARA EL VISOR
                var NewRequestInfo = new RequestInfo
                {
                    CodId = Guid.NewGuid(),
                    
                    CodigoDeSolicitud = newRequest.Code,

                    FechaDeCreacion = newRequest.CreationDate,

                    FechaActualizacion = newRequest.CreationDate,

                    Gestor = newRequest.Suggestion,

                    EtapaDelNegocio = TipoFormulario,

                    CorreoElectronico = Formulario.Contact.Email,

                    Nombre = Formulario.Contact.Nombre,

                    Apellido = Formulario.Contact.Apellido,

                    NumeroIdentificacion = Formulario.Contact.IdentificationNumber,

                    TipoIdentificacion = Formulario.Contact.IdentificationType,

                    Telefono = Formulario.Contact.Phone,

                    NombreNegocio = Formulario.Enterprise.BusinessName,

                    DescripcionNegocio = Formulario.Enterprise.BusinessDescription,

                    ActividadEconomica = Formulario.Enterprise.EconomicActivity,

                    Instagram = Formulario.Enterprise.Instagram,

                    Ruc = Formulario.Enterprise.Ruc,

                    WebSite = Formulario.Enterprise.WebSite,

                    Provincia = Formulario.Enterprise.Province,

                    Distrito = Formulario.Enterprise.District,

                    Corregimiento = Formulario.Enterprise.Corregimiento,

                    ProyeccionVentasMensuales = Convert.ToInt32(Formulario.Enterprise.ProyectedSales).ToString(),

                    VentasMensuales = Convert.ToInt32(Formulario.Enterprise.MonthlySales).ToString(),

                    FechaInicioOperaciones = Convert.ToDateTime(Formulario.Enterprise.OperationsStartDate).ToString(),

                    CuantoChenchenNecesitas = Convert.ToInt32(Formulario.RequestDetail.QuantityToInvert).ToString(),

                    EnQueLoInvertiras = Formulario.RequestDetail.ReasonForMoney,

                    VerificacionCliente = Formulario.RequestDetail.VerifyClient,

                    GestionRealizada = "Por contactar",

                    Etapa = "Por Asignar",

                    UsuarioAsignado = "chenchen",

                    IdChen = newRequest.Id,

                    TipoRequest = newRequest.Type

                };


                //ESTO ES PARA INSERTAR LA DATA 
                _logger.LogInformation("Adding entities to context");
                _context.Requests.Add(newRequest);
                _context.Contacts.Add(Formulario.Contact);
                _context.Enterprises.Add(Formulario.Enterprise);
                _context.RequestDetails.Add(Formulario.RequestDetail);
                _context.RequestInfos.Add(NewRequestInfo);



                //REALIZA EL COMMIT DE LA DATA
                _context.SaveChanges();
                _logger.LogInformation("Changes saved successfully to database");

                return RedirectToAction("EnviarSolicitud", "ToyoNoToy");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "eRROR al Crear el Formulari");
                return View(Formulario);
            }
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
            _logger.LogInformation("SendConfirmationEmail called for email: {Email}", email);
            try
            {
                var fromAddress = new MailAddress("elchenchenvuelve@outlook.com", "ElchenChenVuelve");
                var toAddress = new MailAddress(email);
                const string fromPassword = "Elchenchen507.";
                const string subject = "Confirmación de Correo";
                const string body = "Este es un correo de confirmación.";

                _logger.LogInformation("Attempting to send email to: {Email}", email);

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

                _logger.LogInformation("Email sent successfully to: {Email}", email);
                return Json(new { success = true, message = "Correo enviado exitosamente." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending email to: {Email}", email);
                return Json(new { success = false, message = ex.Message });
            }
        }


    }
}
