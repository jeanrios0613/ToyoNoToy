using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using managerelchenchenvuelve.Models;
using managerelchenchenvuelve.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using DocumentFormat.OpenXml.Office2010.Excel;
using System.Diagnostics;



public class ArchivosController : Controller
{ 
    
    private readonly ToyoNoToyContext   _context;
    private readonly DatabaseConnection _db;
    private readonly string rutaServidor = "..\\wwwroot\\Reportes";


   
    public ArchivosController(ToyoNoToyContext context, DatabaseConnection db)
    {
        _context = context;
        _db      = db;
    }
 

    // GET: Archivos/SubirArchivo
    public IActionResult SubirArchivo(string? ProcessId)
    {    
 
        return View();
    }


}
