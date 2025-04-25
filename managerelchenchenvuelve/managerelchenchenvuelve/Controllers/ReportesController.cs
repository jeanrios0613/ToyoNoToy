using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using managerelchenchenvuelve.Models;

namespace managerelchenchenvuelve.Controllers
{
    public class ReportesController : Controller
    {
        private readonly ToyoNoToyContext _context;

        public ReportesController(ToyoNoToyContext context)
        {
            _context = context;
        }
        public ActionResult Reporte()
        {
            return View();
        }
        public IActionResult Descargar_Reportes()//string? Fecin, string? Fecfin)
        {

            var formularios = _context.ConsultaTodos
                                      //.Where(f => !Fecin.HasValue || f.FechaDeCreacion >= Fecin)
                                     // .Where(f => !Fecfin.HasValue || f.FechaDeCreacion <= Fecfin)
                                      .ToList();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Reporte");

                // Agregar encabezados
                worksheet.Cell(1, 1).Value = "Codigodesolicitud";
                worksheet.Cell(1, 1).Value = "FechadeCreacion";
                worksheet.Cell(1, 1).Value = "FechaActualizacion";
                worksheet.Cell(1, 1).Value = "Gestor";
                worksheet.Cell(1, 1).Value = "EtapadelNegocio";
                worksheet.Cell(1, 1).Value = "CorreoElectronico";
                worksheet.Cell(1, 1).Value = "Nombre";
                worksheet.Cell(1, 1).Value = "Apellido";
                worksheet.Cell(1, 1).Value = "Numeroidentificacion";
                worksheet.Cell(1, 1).Value = "Tipoidentificacion";
                worksheet.Cell(1, 1).Value = "Telefono";
                worksheet.Cell(1, 1).Value = "NombreNegocio";
                worksheet.Cell(1, 1).Value = "Descripcionnegocio";
                worksheet.Cell(1, 1).Value = "Actividadeconomica";
                worksheet.Cell(1, 1).Value = "Instagram";
                worksheet.Cell(1, 1).Value = "RUC";
                worksheet.Cell(1, 1).Value = "WebSite";
                worksheet.Cell(1, 1).Value = "Provincia";
                worksheet.Cell(1, 1).Value = "Distrito";
                worksheet.Cell(1, 1).Value = "corregimiento";
                worksheet.Cell(1, 1).Value = "Proyeccionventasmensuales";
                worksheet.Cell(1, 1).Value = "Ventasmensuales";
                worksheet.Cell(1, 1).Value = "FechaInicioOperaciones";
                worksheet.Cell(1, 1).Value = "CuantoChenchennecesitas";
                worksheet.Cell(1, 1).Value = "Enqueloinvertiras";
                worksheet.Cell(1, 1).Value = "VerificacionCliente";
                worksheet.Cell(1, 1).Value = "GestionRealizada";
                worksheet.Cell(1, 1).Value = "Tipoatencion";
                worksheet.Cell(1, 1).Value = "Porquenocontacto";
                worksheet.Cell(1, 1).Value = "Etapa";
                worksheet.Cell(1, 1).Value = "UsuarioAsignado"; 


                // Agregar datos
                for (int i = 0; i < formularios.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = formularios[i].CodigoDeSolicitud;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].FechaDeCreacion;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].FechaActualizacion;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].Gestor;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].EtapaDelNegocio;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].CorreoElectronico;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].Nombre;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].Apellido;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].NumeroIdentificacion;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].TipoIdentificacion;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].Telefono;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].NombreNegocio;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].DescripcionNegocio;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].ActividadEconomica;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].Instagram;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].Ruc;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].WebSite;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].Provincia;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].Distrito;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].Corregimiento;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].ProyeccionVentasMensuales;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].VentasMensuales;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].FechaInicioOperaciones;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].CuantoChenchenNecesitas;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].EnQueLoInvertiras;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].VerificacionCliente;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].GestionRealizada;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].TipoIdentificacion;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].PorqueNoContacto;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].Etapa;
                    worksheet.Cell(i + 2, 1).Value = formularios[i].UsuarioAsignado; 
                }

                using (var stream = new MemoryStream())
                {   workbook.SaveAs(stream);
                    stream.Position = 0;  
                    var fileName = "Reporte.xlsx";
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
            }
        }

        
    }
}
