using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ElChenVuelve.Models;

public partial class Formulario
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string NombreCompleto
    {
        get
        {
            return $"{Nombre} {Apellido}";
        }
    }

    public string? Email { get; set; }

    public string? Cedula { get; set; }

    public string? Celular { get; set; }

    public string? RedesSociales { get; set; }

    public string? NombreEmpresa { get; set; }

    public string? ActividadEconomica { get; set; }

    public string? DescripcionNegocio { get; set; }

    public decimal? MontoInversion { get; set; }

    public string? DescripcionInversion { get; set; }

    public string? RazonCambio { get; set; }

    public string? IndicaSolicitud { get; set; }

    public bool? DocumentacionAdjunta { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public string? UsuarioAnalista { get; set; }

    public DateTime? FechaAtencion { get; set; }

    public string? UsuarioSupervisor { get; set; }

    public DateTime? FechaAprobacion { get; set; }

    public string? Localidad { get; set; }
}

public partial class Actividad
{
    public int Id { get; set; }

    public string NombreActividad { get; set; } = null!;
}
