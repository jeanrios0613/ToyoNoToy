﻿using System.Drawing.Printing;
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
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http.HttpResults;

namespace managerelchenchenvuelve.Controllers
{
    //[Authorize]
    public class ProcessController : Controller
    {
        private readonly DatabaseConnection _db;
        private readonly ToyoNoToyContext _context;
        private readonly ILogger<ProcessController> _logger;

        public ProcessController(ToyoNoToyContext context, DatabaseConnection db, ILogger<ProcessController> logger)
        {
            _context = context;
            _logger = logger;
            _db = db;
        }

        // GET: ProcessController
        public ActionResult Index(string? id = null, int page = 1, int pageSize = 10, string? tarea = "P", string? search = null, string? business = null,string?  ListUser = null)
        {
            var username  = HttpContext.Session.GetString("UserName");
            var Userss    = HttpContext.Session.GetString("Userss");
            var Roles     = HttpContext.Session.GetString("Roles");

			try
            {
                _logger.LogInformation("Accediendo a Process/Index");

                // Obtener el nombre de usuario de la sesión
                _logger.LogInformation("Usuario en sesión: {Username}", username);

                if (string.IsNullOrEmpty(username))
                {
                    _logger.LogWarning("No se encontró usuario en la sesión");
                    return RedirectToAction("Login", "Account");
                }
 

					ViewBag.nombreUsuario  = username;

                List<DatosReca> Datos = new List<DatosReca>();



              

                string query = @"SELECT *
                                 FROM (SELECT  CONCAT( RI.codigo_de_solicitud, '  ', RI.NOMBRE,'  ',RI.APELLIDO,'  ', RI.NUMERO_IDENTIFICACION,'  ',RI.GESTOR) AS CompletaActividad, 
                                       FORMAT(SWITCHOFFSET(RI.Fecha_de_creacion, '-05:00'),'MMMM dd, yyyy hh:mm tt','es-es') AS FechaFormateada,  
                                       CASE 
                                       WHEN DATEDIFF(MINUTE, RI.Fecha_de_creacion, SYSDATETIMEOFFSET()) < 60 
                                        THEN 'hace ' + CAST(DATEDIFF(MINUTE,RI.Fecha_de_creacion, SYSDATETIMEOFFSET()) AS VARCHAR) + ' minutos'
                                        WHEN DATEDIFF(HOUR, RI.Fecha_de_creacion, SYSDATETIMEOFFSET()) < 24 
                                        THEN 'hace ' + CAST(DATEDIFF(HOUR,RI.Fecha_de_creacion, SYSDATETIMEOFFSET()) AS VARCHAR) + ' horas'
                                        ELSE 
                                            'hace ' + CAST(DATEDIFF(DAY, RI.Fecha_de_creacion, SYSDATETIMEOFFSET()) AS VARCHAR) + ' días'
                                            END AS TiempoTranscurrido,
                                 
                                        TRY_CAST(CASE 
                                        WHEN DATEDIFF(MINUTE, RI.Fecha_de_creacion, SYSDATETIMEOFFSET()) < 60 
                                            THEN CAST(DATEDIFF(MINUTE,RI.Fecha_de_creacion, SYSDATETIMEOFFSET()) AS VARCHAR)
                                        WHEN DATEDIFF(HOUR, RI.Fecha_de_creacion, SYSDATETIMEOFFSET()) < 24 
                                            THEN CAST(DATEDIFF(HOUR,RI.Fecha_de_creacion, SYSDATETIMEOFFSET()) AS VARCHAR)
                                        ELSE 
                                            CAST(DATEDIFF(DAY, RI.Fecha_de_creacion, SYSDATETIMEOFFSET()) AS VARCHAR) 
                                            END as int)  AS Tiempo,
 
                                        RI.* 

                                      FROM  [dbo].[Request_info] as RI ) as REQS ";

                //Se utiliza este filtro para poder impletar el buscador 
                if (search == null)
                {
                    query += " WHERE GESTOR = 'Gestión directa de Ampyme' ";
                              

                }
                else if (search != null)
                {

                    query += "WHERE CompletaActividad LIKE '%" + search + "%'";

                }


                if (!string.IsNullOrEmpty(ListUser))
                {
                    query += " AND usuario_asignado in  ( "+ListUser+" )";
                }
                else
                {

                    query += " AND usuario_asignado = COALESCE(@username, usuario_asignado)";
                }


                if (tarea == "C")
                {
                    query += " AND ETAPA = 'Completada'";

                }
                if (tarea == "P")
                {

                    query += " AND ETAPA != 'Completada'";
                }

                if (!string.IsNullOrEmpty(business))
                {
                    query += " AND Etapa_del_Negocio in (" + business + ")";
                }

                query += " ORDER BY fecha_de_creacion desc " +
                             " OFFSET @Offset ROWS " +
                             " FETCH NEXT @PageSize ROWS ONLY";

                SqlParameter[] parameters = new SqlParameter[]
                {   
                    new SqlParameter("@username",Userss), 
                    new SqlParameter("@Offset", (page - 1) * pageSize),
                    new SqlParameter("@PageSize", pageSize)
                };

                DataTable result = _db.ExecuteQuery(query, parameters);

                /////////////////////////////////////********** TOTAL PARA PAGINATION ************************//////////////////////////////////////////////

                string CountQuery = @"SELECT  * 
                                      FROM ( SELECT  CONCAT( RI.codigo_de_solicitud, '  ', RI.NOMBRE,'  ',RI.APELLIDO,'  ', RI.NUMERO_IDENTIFICACION,'  ',RI.GESTOR) AS CompletaActividad, 
                                            FORMAT(SWITCHOFFSET(RI.Fecha_de_creacion, '-05:00'),'MMMM dd, yyyy hh:mm tt','es-es') AS FechaFormateada,  
                                            CASE 
                                            WHEN DATEDIFF(MINUTE, RI.Fecha_de_creacion, SYSDATETIMEOFFSET()) < 60 
                                            THEN 'hace ' + CAST(DATEDIFF(MINUTE,RI.Fecha_de_creacion, SYSDATETIMEOFFSET()) AS VARCHAR) + ' minutos'
                                            WHEN DATEDIFF(HOUR, RI.Fecha_de_creacion, SYSDATETIMEOFFSET()) < 24 
                                            THEN 'hace ' + CAST(DATEDIFF(HOUR,RI.Fecha_de_creacion, SYSDATETIMEOFFSET()) AS VARCHAR) + ' horas'
                                            ELSE 
                                                'hace ' + CAST(DATEDIFF(DAY, RI.Fecha_de_creacion, SYSDATETIMEOFFSET()) AS VARCHAR) + ' días'
                                                END AS TiempoTranscurrido,
                                 
                                            TRY_CAST(CASE 
                                            WHEN DATEDIFF(MINUTE, RI.Fecha_de_creacion, SYSDATETIMEOFFSET()) < 60 
                                                THEN CAST(DATEDIFF(MINUTE,RI.Fecha_de_creacion, SYSDATETIMEOFFSET()) AS VARCHAR)
                                            WHEN DATEDIFF(HOUR, RI.Fecha_de_creacion, SYSDATETIMEOFFSET()) < 24 
                                                THEN CAST(DATEDIFF(HOUR,RI.Fecha_de_creacion, SYSDATETIMEOFFSET()) AS VARCHAR)
                                            ELSE 
                                                CAST(DATEDIFF(DAY, RI.Fecha_de_creacion, SYSDATETIMEOFFSET()) AS VARCHAR) 
                                                END as int)  AS Tiempo,
 
                                            RI.* 

                                            FROM  [dbo].[Request_info] as RI ) as REQS ";

                //Se utiliza este filtro para poder impletar el buscador 
                if (search == null)
                {
                    CountQuery += " WHERE GESTOR = 'Gestión directa de Ampyme'";

                }
                else if (search != null)
                {

                    CountQuery += " WHERE CompletaActividad LIKE '%" + search + "%'";

                }

                if (!string.IsNullOrEmpty(ListUser))
                {
                    CountQuery += " AND usuario_asignado in  ( " + ListUser + " )";
                }
                else
                {

                    CountQuery += " AND usuario_asignado = COALESCE(@username, usuario_asignado)";
                }

                if (tarea == "C")
                {
                    CountQuery += " AND ETAPA = 'Completada'";

                }
                if (tarea == "P")
                {

                    CountQuery += " AND ETAPA != 'Completada'";
                }

                if (!string.IsNullOrEmpty(business))
                {
                    CountQuery += " AND Etapa_del_Negocio in (" + business + ")";
                }


                CountQuery += " ORDER BY fecha_de_creacion desc " ;

                SqlParameter[] Countparameters = new SqlParameter[]
                {
                    new SqlParameter("@username",Userss) 

                };

                DataTable Countresult = _db.ExecuteQuery(CountQuery, Countparameters);

                int totalCount = Countresult.Rows.Count;



                foreach (DataRow row in result.Rows)
                    {
                        Datos.Add(new DatosReca
                        {   Id = row["codigo_de_solicitud"].ToString(),
                            gestor = row["GESTOR"].ToString(),
                            Etapa = row["Etapa"].ToString(),
                            CompletaActividad = row["CompletaActividad"].ToString(),
                            FechaFormateada = row["FechaFormateada"].ToString(),
                            TiempoTranscurrido = row["TiempoTranscurrido"].ToString(),
                            UserName = row["usuario_asignado"].ToString(),
                            CodigoDeSolicitud = row["codigo_de_solicitud"].ToString(),
                            Tiempo =Convert.ToInt32(row["Tiempo"].ToString())
                        });
                    }
                    


                    ViewBag.Cantidad     = Math.Min(page * pageSize, totalCount);
                    ViewBag.TotalQuery   = totalCount;
                    ViewBag.ViewQuery    = page * 10;
                    ViewBag.AdminUser    = HttpContext.Session.GetString("Roles");
                    ViewBag.TotalPages   = (int)Math.Ceiling((double)totalCount / pageSize);
                    ViewBag.CurrentPage  = page;
                    ViewBag.DatosReca    = Datos;
                    ViewBag.Tarea        = tarea;
                    ViewBag.listUsers    = ObtenerUsuariosAmpyme();


                    _logger.LogInformation("Obteniendo lista de formularios");
                    return View(Datos);
                 
                 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en Process/Index");
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: ProcessController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProcessController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProcessController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProcessController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProcessController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProcessController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProcessController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        public List<AsignacionClass> ObtenerUsuariosAmpyme() {

            List<AsignacionClass> Userlist = new List<AsignacionClass>();

            String Datas = @"select upper(SUBSTRING(us.names,1,1)) letters, us.username, (names+' '+Lastname) nombrecompleto 
                            from       [dbo].[Users]     as US
                            inner join [dbo].[UserRoles] as UR
                                    on us.id = UR.Userid
                            inner join [dbo].[Roles]     as RL 
                                    ON rl.id = ur.roleid
                            where rl.rolname in ('GESTIÓN DE AMPYME','ADMINISTRADOR') 
                            and    us.Status = 1
                            order by 2"
            ;

            DataTable listUser = _db.ExecuteQuery(Datas);

            foreach (DataRow row in listUser.Rows)
            {
                Userlist.Add(new AsignacionClass
                {
                    Usuario = row["username"].ToString(),
                    NombreCompleto = row["nombrecompleto"].ToString(),
                    Letters = row["letters"].ToString()
                });
            }
            
            return Userlist;

        }



        [HttpPost]
        public IActionResult AsignarTareas([FromBody] AsignacionClass model)
        {
            try
            {
                if (model == null || model.Usuario == null || string.IsNullOrEmpty(model.Usuario) || model.Ids == null || !model.Ids.Any())
                {
                    return BadRequest("Datos incompletos");
                }

                foreach (var codigo in model.Ids)
                {
                    string updateQuery = @"UPDATE dbo.Request_Info 
                                   SET usuario_asignado = @usuario 
                                   WHERE codigo_de_solicitud = @codigo";

                    _logger.LogInformation("hacer update ************************: {codigo}", codigo);
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("@usuario", model.Usuario),
                        new SqlParameter("@codigo", codigo)
                    };

                    _db.ExecuteNonQuery(updateQuery, parameters);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                // opcional: _logger.LogError(ex, "Error al asignar tareas");
                return StatusCode(500, "Error al asignar tareas");
            }
        }


    }
}
