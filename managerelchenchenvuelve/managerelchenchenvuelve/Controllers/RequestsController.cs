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

            string query = @"SELECT * FROM [ToyNoToy].[dbo].[Request_Info] WHERE Codigo_de_solicitud = @Codigo";

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
                {    
                    Code = row["Codigo_de_solicitud"].ToString(),
                    CreationDate = DateTime.Parse(row["Fecha_de_Creacion"].ToString()), 
                    Gestor = row["Gestor"].ToString(),
                    Etapa_del_Negocio = row["Etapa_del_Negocio"].ToString(),
                    Etapa = row["Etapa"].ToString(),
                    Email = row["Correo_Electronico"].ToString(),
                    Nombre = row["Nombre"].ToString(),
                    Apellido = row["Apellido"].ToString(),
                    IdentificationNumber = row["Numero_identificacion"].ToString(),
                    IdentificationType = row["Tipo_identificacion"].ToString(),
                    Phone = row["Telefono"].ToString(),
                    BusinessName = row["Nombre_Negocio"].ToString(),
                    BusinessDescription = row["Descripcion_negocio"].ToString(), 
                    EconomicActivity = row["Actividad_economica"].ToString(),
                    Instagram = row["Instagram"].ToString(),
                    Ruc = row["Ruc"].ToString(),
                    WebSite = row["Web_Site"].ToString(),
                    Corregimiento = row["corregimiento"].ToString(),
                    District = row["Distrito"].ToString(),
                    Province = row["Provincia"].ToString(),
                    MonthlySales = string.IsNullOrEmpty(row["Ventas_mensuales"].ToString()) ? (decimal?)null : decimal.Parse(row["Ventas_mensuales"].ToString()),
                    ProyectedSales = string.IsNullOrEmpty(row["Proyeccion_ventas_mensuales"].ToString()) ? (decimal?)null : decimal.Parse(row["Proyeccion_ventas_mensuales"].ToString()),
                    QuantityToInvert = string.IsNullOrEmpty(row["Cuanto_Chenchen_necesitas"].ToString()) ? (decimal?)null : decimal.Parse(row["Cuanto_Chenchen_necesitas"].ToString()),
                    OperationsStartDate = DateTime.Parse(row["Fecha_Inicio_Operaciones"].ToString()), 
                    ReasonForMoney = row["En_que_lo_invertiras"].ToString(),
                    VerifyClient = row["Verificacion_Cliente"].ToString(),
                    GestionRealizada = row["Gestion_Realizada"].ToString(),
                    TipoAtencion = row["Tipo_atencion"].ToString(),
                    ContactReason = row["Porque_no_contacto"].ToString(),
                    Usuario_Asignado = row["Usuario_Asignado"].ToString(),
                   
                };
            }

            if (info == null)
            {
                return NotFound();
            }
            
            ViewBag.Codigo = id;
            return View(info);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult solicitud(RequestClass formData)
        {
            try
            {
                _logger.LogInformation("Iniciando procesamiento de solicitud. Datos recibidos: {@formData}", formData);

              

                // Get the current user from session,Validation Session
                var username = HttpContext.Session.GetString("UserName");
                if (string.IsNullOrEmpty(username))
                {
                    _logger.LogWarning("No se encontró usuario en la sesión");
                    return RedirectToAction("Login", "Account");
                }

                // Update the process status in the database
                string updateQuery = @"update ToyNoToy.dbo.Request_Info
                                 set    Verificacion_Cliente = @verifica
                                       ,Gestion_Realizada    = @gestion
                                       ,Tipo_atencion        = @tipo
                                       ,Porque_no_contacto   = @contacto
                                       ,Etapa  = 'Completada'
                                 WHERE Codigo_de_solicitud   = @CodigoDeSolicitud";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@verifica", formData.VerifyClient ?? (object)DBNull.Value),
                    new SqlParameter("@gestion", formData.GestionRealizada ?? (object)DBNull.Value),
                    new SqlParameter("@tipo", formData.TipoAtencion ?? (object)DBNull.Value),
                    new SqlParameter("@contacto", formData.ContactReason ?? (object)DBNull.Value),
                    new SqlParameter("@CodigoDeSolicitud", formData.Code)
                };

                _logger.LogInformation("Ejecutando query con parámetros: {@parameters}", 
                    parameters.Select(p => new { p.ParameterName, p.Value }));

                int affected = _db.ExecuteNonQuery(updateQuery, parameters);
                _logger.LogInformation("Filas afectadas: {affected}", affected);

                if (affected > 0)
                {
                    _logger.LogInformation("Actualización exitosa");
                    return RedirectToAction("Index", "Process");
                }
                else
                {
                    _logger.LogWarning("No se actualizó ningún registro");
                    ModelState.AddModelError("", "No se pudo actualizar el registro.");
                    return View(formData);
                }

                 
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
        public ActionResult AddComment(string requestCode, string commentText, string gestor, string Etapa, string TypeRequest)
        {
            try
            {
                _logger.LogInformation("Iniciando inserción de comentario para solicitud: {requestCode}", requestCode);
                _logger.LogInformation("Revisa Valores insertados : {TypeRequest}", TypeRequest);

                // Get the current user from session
                var username = HttpContext.Session.GetString("UserName");
                if (string.IsNullOrEmpty(username))
                {
                    _logger.LogWarning("No se encontró usuario en la sesión");
                    return Json(new { success = false, message = "Usuario no autenticado" });
                }

                // Insert the comment into the database
                string insertQuery = @"INSERT INTO [ToyNoToy].[dbo].[Comments] 
                                     (id, [ProcessInstanceId], [Message], [CreatedBy],[CreatedAt],[StageName]) 
                                     VALUES (@id,@ProcessInstanceId, @Message, @createdBy, @createdAt ,@StageName)";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", Guid.NewGuid()),
                    new SqlParameter("@ProcessInstanceId", requestCode),
                    new SqlParameter("@Message", commentText),
                    new SqlParameter("@CreatedBy", username),
                    new SqlParameter("@createdAt", DateTime.Now),
                    new SqlParameter("@StageName", gestor)
                };


                if (TypeRequest == "AprovedRequest") {
                 string UpdateQuery = @"UPDATE [dbo].[Request_info] 
                                       SET     Etapa  = 'Re-Abrir Solicitud', 
                                               Usuario_Asignado = null
                                       WHERE  Codigo_de_solicitud = @codigo;";


                    SqlParameter[] parameters2 = new SqlParameter[] {
                        new SqlParameter("@codigo", requestCode)

                    };

                 _db.ExecuteNonQuery(UpdateQuery, parameters2);
                 _logger.LogWarning("update AprovedRequest Realizado para " + requestCode);
                }


                if (TypeRequest == "ChangeRequest")
                {
                    string UpdateQuery2 = @"UPDATE [dbo].[Request_info] 
                                       SET     Etapa  = 'Cambio de Solicitud', 
                                               Usuario_Asignado = null
                                       WHERE  Codigo_de_solicitud = @codigo;";


                    SqlParameter[] parameters3 = new SqlParameter[] {
                        new SqlParameter("@codigo", requestCode)

                    };

                    _logger.LogWarning("update ChangeRequest Realizado para " + requestCode);
                    _db.ExecuteNonQuery(UpdateQuery2, parameters3);

                }



                int affected = _db.ExecuteNonQuery(insertQuery, parameters);
                _logger.LogInformation("Filas afectadas al insertar comentario: {affected}", affected);

                if (affected > 0)
                {
                    _logger.LogInformation("Comentario insertado exitosamente");
                    return Json(new { success = true, message = "Comentario agregado exitosamente" });
                }
                else
                {
                    _logger.LogWarning("No se pudo insertar el comentario");
                    return Json(new { success = false, message = "No se pudo agregar el comentario" });
                }


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al insertar comentario");
                return Json(new { success = false, message = "Error al procesar el comentario" });
            }
        }

    }
}

