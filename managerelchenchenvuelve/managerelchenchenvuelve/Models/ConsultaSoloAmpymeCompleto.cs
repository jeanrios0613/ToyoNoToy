using System;
using System.Collections.Generic;

namespace managerelchenchenvuelve.Models;

public partial class ConsultaSoloAmpymeCompleto
{
    public string? CodigoDeSolicitud { get; set; }

    public string? FechaDeCreacion { get; set; }

    public DateTimeOffset FechaDeActualizacion { get; set; }

    public string? Gestor { get; set; }

    public string EtapaDelNegocio { get; set; } = null!;

    public string? CorreoElectronico { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? NumeroDeIdentificacion { get; set; }

    public string? TipoDeIdentificacion { get; set; }

    public string? Teléfono { get; set; }

    public string? NombreDelNegocio { get; set; }

    public string? DescripcionDelNegocio { get; set; }

    public string? ActividadEconómica { get; set; }

    public string? Instagram { get; set; }

    public string? Ruc { get; set; }

    public string? WebSite { get; set; }

    public string? Provincia { get; set; }

    public string? Distrito { get; set; }

    public string? Corregimiento { get; set; }

    public string? ProyeccionDeVentasMensuales { get; set; }

    public string? VentasMensuales { get; set; }

    public string? FechaDeInicioDeOperaciones { get; set; }

    public string? CuantoChenchenNecesitas { get; set; }

    public string? EnQueLoInvertiras { get; set; }

    public string? VerificacionDelCliente { get; set; }

    public string? GestionRealizada { get; set; }

    public string? TipoDeAtencion { get; set; }

    public string? PorQueNoSeContacto { get; set; }

    public string? Etapa { get; set; }

    public string? UsuarioAsignado { get; set; }

    public Guid Id { get; set; }
 

    public string CompletaActividad { get; set; } = null!;

    public string? FechaFormateada { get; set; }

    public string? TiempoTranscurrido { get; set; }
}
