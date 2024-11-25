using System;
using System.Collections.Generic;

namespace ElChenVuelve.Models;

public partial class Usuario
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public DateTime? FechaCreacion { get; set; }

    public DateTime? UltimoAcceso { get; set; }

    public string? Rol { get; set; }

    public string? Estado { get; set; }

    public DateTime? FechaUltimaActualizacion { get; set; }
}
