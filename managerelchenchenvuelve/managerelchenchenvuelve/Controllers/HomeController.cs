using System.Configuration;
using System.Data;
using System.Diagnostics;
using FluentAssertions.Common;
using managerelchenchenvuelve.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace managerelchenchenvuelve.Controllers
{
    public class HomeController: Controller
    {
       

        // Constructor con inyección de dependencias para IConfiguration
       
        public IActionResult Index()
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
    }
}
