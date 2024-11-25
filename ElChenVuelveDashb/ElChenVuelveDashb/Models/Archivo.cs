using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ElChenVuelveDashb.Models
{
    public partial class Archivo
    {
        public int Id { get; set; }

        public string? Descripcion { get; set; }

        public string? Ruta { get; set; }

    }

}
