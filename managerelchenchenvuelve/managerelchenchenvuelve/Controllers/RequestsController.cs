using System.Drawing.Printing;
using System.Security.Claims;
using BootstrapBlazor.Components;
using managerelchenchenvuelve.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using managerelchenchenvuelve.Services;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http.HttpResults;



namespace managerelchenchenvuelve.Controllers
{
    public class RequestsController : Controller
    {

        private readonly DatabaseConnection _db;
        private readonly ToyoNoToyContext _context;
        private readonly ILogger<ProcessController> _logger;

        public RequestsController(ToyoNoToyContext context, DatabaseConnection db, ILogger<ProcessController> logger)
        {
            _context = context;
            _logger = logger;
            _db = db;
        }

        // GET: RequestsController/solicitud
        public ActionResult solicitud(string? id)
        {
            var username = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(username))
            {
                _logger.LogWarning("No se encontró usuario en la sesión");
                return RedirectToAction("Login", "Account");
            }

            string query = @"SELECT * FROM [PoluxDb].[dbo].[VIEW_DATA_AMPYME] WHERE CODE = @Codigo";

            SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Codigo",id)
                };

            DataTable dt = _db.ExecuteQuery(query,parameters); ;

            RequestClass info = null;

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                info = new RequestClass
                {   id = row["id"].ToString(),
                    Code = row["Code"].ToString(),
                    CreationDate = DateTime.Parse(row["CreationDate"].ToString()),
                    TipoSolicitud = int.Parse(row["TipoSolicitud"].ToString()),
                    Gestor = row["Gestor"].ToString(),
                    Etapa_del_Negocio = row["Etapa_del_Negocio"].ToString(),
                    Etapa = row["Etapa"].ToString(),
                    Email = row["Email"].ToString(),
                    FullName = row["FullName"].ToString(),
                    IdentificationNumber = row["IdentificationNumber"].ToString(),
                    IdentificationType = row["IdentificationType"].ToString(),
                    Phone = row["Phone"].ToString(),
                    BusinessName = row["BusinessName"].ToString(),
                    BusinessDescription = row["BusinessDescription"].ToString(),
                    BusinessTime = row["BusinessTime"].ToString(),
                    EconomicActivity = row["EconomicActivity"].ToString(),
                    Instagram = row["Instagram"].ToString(),
                    Ruc = row["Ruc"].ToString(),
                    WebSite = row["WebSite"].ToString(),
                    Corregimiento = row["Corregimiento"].ToString(),
                    District = row["District"].ToString(),
                    Province = row["Province"].ToString(),
                    MonthlySales = string.IsNullOrEmpty(row["MonthlySales"].ToString()) ? (decimal?)null : decimal.Parse(row["MonthlySales"].ToString()),
                    ProyectedSales = string.IsNullOrEmpty(row["ProyectedSales"].ToString()) ? (decimal?)null : decimal.Parse(row["ProyectedSales"].ToString()),
                    QuantityToInvert = string.IsNullOrEmpty(row["QuantityToInvert"].ToString()) ? (decimal?)null : decimal.Parse(row["QuantityToInvert"].ToString()),
                    ReasonForMoney = row["ReasonForMoney"].ToString(),
                    VerifyClient = row["VerifyClient"].ToString(),
                    ManagementExecuted = row["ManagementExecuted"].ToString(),
                    TipoAtencion = row["TipoAtencion"].ToString(),
                    ContactReason = row["ContactReason"].ToString(),
                    Codigo = row["Codigo"].ToString(),
                    Datos = row["Datos"].ToString(),
                    DataObject = row["DataObject"].ToString(),
                    Username = row["USERNAME"].ToString(),
                    Completed = string.IsNullOrEmpty(row["Completed"].ToString()) ? (DateTimeOffset?)null : DateTimeOffset.Parse(row["Completed"].ToString()),
                    Created = string.IsNullOrEmpty(row["Created"].ToString()) ? (DateTimeOffset?)null : DateTimeOffset.Parse(row["Created"].ToString()),
                    Updated = string.IsNullOrEmpty(row["Updated"].ToString()) ? (DateTimeOffset?)null : DateTimeOffset.Parse(row["Updated"].ToString()),
                    ActivityCreated = string.IsNullOrEmpty(row["ActivityCreated"].ToString()) ? (DateTimeOffset?)null : DateTimeOffset.Parse(row["ActivityCreated"].ToString()),
                    Estado = int.Parse(row["ESTADO"].ToString()),
                    InstanceDefinition = string.IsNullOrEmpty(row["InstanceDefinition"].ToString()) ? (Guid?)null : Guid.Parse(row["InstanceDefinition"].ToString()),
                    FechaFormateada = row["FechaFormateada"].ToString(),
                    CompletaActividad = row["CompletaActividad"].ToString(),
                    TiempoTranscurrido = row["TiempoTranscurrido"].ToString(),
                    OperationsStartDate = string.IsNullOrEmpty(row["OperationsStartDate"].ToString()) ? (DateTimeOffset?)null : DateTimeOffset.Parse(row["OperationsStartDate"].ToString()),
                };
            }

            if (info == null)
            {
                return NotFound();
            }
            
            ViewBag.Codigo = id;
            return View(info);
        }

        // POST: RequestsController/solicitud
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> solicitud(ProcessFormData formData)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(formData);
                }

                // Get the current user from session,Validation Session
                var username = HttpContext.Session.GetString("UserName");
                if (string.IsNullOrEmpty(username))
                {
                    _logger.LogWarning("No se encontró usuario en la sesión");
                    return RedirectToAction("Login", "Account");
                }

                // Get the existing process instance
                var processInstances = await _context.RequestClasses
                    .FirstOrDefaultAsync(p => p.Code == formData.CodigoDeSolicitud);

                
                await _context.SaveChangesAsync();

                // Update the process status in the database
                string updateQuery = @"
                    UPDATE [PoluxDb].[dbo].[VIEW_DATA_AMPYME]
                    SET ETAPA = @Etapa,
                        GESTOR = @Gestor
                    WHERE [Code] = @CodigoDeSolicitud";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Etapa", "Completada"),
                    new SqlParameter("@Gestor", username),
                    new SqlParameter("@CodigoDeSolicitud", formData.CodigoDeSolicitud)
                };

                _db.ExecuteNonQuery(updateQuery, parameters);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al procesar la solicitud");
                ModelState.AddModelError("", "Ocurrió un error al procesar la solicitud.");
                return View(formData);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AprobarSolicitud(string codigoSolicitud, string codigoId, string gestionreal)
        {
            try
            {
                var username = HttpContext.Session.GetString("UserName");
                if (string.IsNullOrEmpty(username))
                {
                    _logger.LogWarning("No se encontró usuario en la sesión");
                    return RedirectToAction("Login", "Account");
                }

                // Update the process status in the database
                string updateQuery = @"update [ToyNoToy].[dbo].[RequestDetails] 
                                      set managementExecuted = @GestionRealiza
                                      where RequestId = @codigoId";

                SqlParameter[] parameters = new SqlParameter[]
                {  
                    new SqlParameter("@codigoId", codigoId),
                    new SqlParameter("@GestionRealiza", gestionreal)
                };

                _db.ExecuteNonQuery(updateQuery, parameters);

                // Create a new action log entry
                var actionLog = new ActionLog
                {
                    Id = Guid.NewGuid(), 
                    CreatedDate = DateTimeOffset.Now,
                    ActionType = 2, // 2 represents approval action
                    ActionLogData = System.Text.Json.JsonSerializer.Serialize(new
                    {
                        CodigoSolicitud = codigoSolicitud,
                        Accion = "Aprobación",
                        Fecha = DateTime.Now
                    })
                };

                _context.ActionLogs.Add(actionLog);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Solicitud aprobada exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al aprobar la solicitud");
                return Json(new { success = false, message = "Error al aprobar la solicitud" });
            }
        }

    }
}

