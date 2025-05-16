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

            RequestInfo info = null;

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                info = new RequestInfo
                {
                    CodigoDeSolicitud = row["Codigo_de_solicitud"].ToString(),
                    FechaDeCreacion = string.IsNullOrEmpty(row["Fecha_de_Creacion"].ToString()) ? (DateTimeOffset?)null : DateTimeOffset.Parse(row["Fecha_de_Creacion"].ToString()),
                    FechaActualizacion = row["Fecha_Actualizacion"].ToString(),
                    Gestor = row["Gestor"].ToString(),
                    EtapaDelNegocio = row["Etapa_del_Negocio"].ToString(),
                    CorreoElectronico = row["Correo_Electronico"].ToString(),
                    Nombre = row["Nombre"].ToString(),
                    Apellido = row["Apellido"].ToString(),
                    NumeroIdentificacion = row["Numero_identificacion"].ToString(),
                    TipoIdentificacion = row["Tipo_identificacion"].ToString(),
                    Telefono = row["Telefono"].ToString(),
                    NombreNegocio = row["Nombre_Negocio"].ToString(),
                    DescripcionNegocio = row["Descripcion_negocio"].ToString(),
                    ActividadEconomica = row["Actividad_economica"].ToString(),
                    Instagram = row["Instagram"].ToString(),
                    Ruc = row["RUC"].ToString(),
                    WebSite = row["Web_Site"].ToString(),
                    Provincia = row["Provincia"].ToString(),
                    Distrito = row["Distrito"].ToString(),
                    Corregimiento = row["corregimiento"].ToString(),
                    ProyeccionVentasMensuales = row["Proyeccion_ventas_mensuales"].ToString(),
                    VentasMensuales = row["Ventas_mensuales"].ToString(),
                    FechaInicioOperaciones = row["Fecha_Inicio_Operaciones"].ToString(),
                    CuantoChenchenNecesitas = row["Cuanto_Chenchen_necesitas"].ToString(),
                    EnQueLoInvertiras = row["En_que_lo_invertiras"].ToString(),
                    VerificacionCliente = row["Verificacion_Cliente"].ToString(),
                    GestionRealizada = row["Gestion_Realizada"].ToString(),
                    TipoAtencion = row["Tipo_atencion"].ToString(),
                    PorqueNoContacto = row["Porque_no_contacto"].ToString(),
                    Etapa = row["Etapa"].ToString(),
                    UsuarioAsignado = row["Usuario_Asignado"].ToString(),


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
        public ActionResult solicitud(RequestInfo formData)
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
                    new SqlParameter("@verifica", formData.VerificacionCliente ?? (object)DBNull.Value),
                    new SqlParameter("@gestion", formData.GestionRealizada ?? (object)DBNull.Value),
                    new SqlParameter("@tipo", formData.TipoAtencion ?? (object)DBNull.Value),
                    new SqlParameter("@contacto", formData.PorqueNoContacto ?? (object)DBNull.Value),
                    new SqlParameter("@CodigoDeSolicitud", formData.CodigoDeSolicitud)
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


				string UpdateQuery = @"UPDATE [dbo].[Request_info]  SET ";
				if (TypeRequest == "AprovedChange")
				{
					UpdateQuery += "Etapa = 'Re-Abrir Solicitud'," +
								   "Usuario_Asignado = 'chenchen'";
				}
				else if (TypeRequest == "OpenRequest")
				{

					UpdateQuery += "Etapa = '5-Gestión Ampyme'" +
								   "Usuario_Asignado = 'chenchen'";

				}
				else if (TypeRequest == "ChangeRequest")
				{

					UpdateQuery += "Etapa = 'Solicitar Cambio'" +
								   "Usuario_Asignado = 'chenchen'";

				}
				else if (TypeRequest == "AprovedChange")
				{

					UpdateQuery += "Etapa = 'Aprobar Cambio'," +
								   " Usuario_Asignado = null," +
								   " Gestor = 'Gestión Caja de Ahorros'";
				}

				UpdateQuery += " WHERE Codigo_de_solicitud = @codigo; ";
				SqlParameter[] parameters2 = new SqlParameter[] {
	               new SqlParameter("@codigo", requestCode)   };

				_db.ExecuteNonQuery(UpdateQuery, parameters2);
				_logger.LogWarning("update AprovedRequest Realizado para " + requestCode);


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

