using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace elchenchenvuelvecy.Models;

public partial class Contact
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    private string? Nombre { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    private string? Apellido { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Este campo es obligatorio")]
    public string FullName
	{
		get => $"{Nombre} {Apellido}";
		set { }
	}

	[Required(ErrorMessage = "Este campo es obligatorio")]
    public string? IdentificationNumber { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    public string? IdentificationType { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    public string? Phone { get; set; }

    public DateTime CreationDate { get; set; }

    public Guid RequestId { get; set; }

    public virtual Request Request { get; set; } = null!; 
}
