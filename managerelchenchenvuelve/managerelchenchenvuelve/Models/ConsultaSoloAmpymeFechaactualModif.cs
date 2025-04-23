using System;
using System.Collections.Generic;

namespace managerelchenchenvuelve.Models;

public partial class ConsultaSoloAmpymeFechaactualModif
{
    public string? CódigoDeSolicitud { get; set; }

    public string? FechaDeCreación { get; set; }

    public string? FechaDeActualizacion { get; set; }

    public string? Gestor { get; set; }

    public string EtapaDelNegocio { get; set; } = null!;

    public string? CorreoElectronico { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? NúmeroDeIdentificación { get; set; }

    public string? TipoDeIdentificación { get; set; }

    public string? Teléfono { get; set; }

    public string? NombreDelNegocio { get; set; }

    public string? DescripciónDelNegocio { get; set; }

    public string? ActividadEconómica { get; set; }

    public string? Instagram { get; set; }

    public string? Ruc { get; set; }

    public string? WebSite { get; set; }

    public string? Provincia { get; set; }

    public string? Distrito { get; set; }

    public string? Corregimiento { get; set; }

    public string? ProyecciónDeVentasMensuales { get; set; }

    public string? VentasMensuales { get; set; }

    public string? FechaDeInicioDeOperaciones { get; set; }

    public string? CuantoChenchenNecesitas { get; set; }

    public string? EnQuéLoInvertirás { get; set; }

    public string? VerificaciónDelCliente { get; set; }

    public string? GestiónRealizada { get; set; }

    public string? TipoDeAtencion { get; set; }

    public string? PorQueNoSeContacto { get; set; }

    public string? Etapa { get; set; }

    public string? UsuarioAsignado { get; set; }

    public Guid Id { get; set; }
}
