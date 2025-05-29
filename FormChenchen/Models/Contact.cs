using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FormChenchen.Models;

public partial class Contact
{

    public Guid Id { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    public string Email { get; set; } = null!;

    [NotMapped]
    [Required(ErrorMessage = "Este campo es obligatorio")]
    public string? Nombre { get; set; }
    [NotMapped]
    [Required(ErrorMessage = "Este campo es obligatorio")]
    public string? Apellido { get; set; }


    [Required(ErrorMessage = "Este campo es obligatorio")]
    public string FullName
    {
        get => $"{Nombre} {Apellido}";
        set { }
    }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    public string IdentificationNumber { get; set; } = null!;

    [Required(ErrorMessage = "Este campo es obligatorio")]
    public string IdentificationType { get; set; } = null!;

    [Required(ErrorMessage = "Este campo es obligatorio")]
    public string Phone { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public Guid RequestId { get; set; }

    public virtual Request Request { get; set; } = null!;
}