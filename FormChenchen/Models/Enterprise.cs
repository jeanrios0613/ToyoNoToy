using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FormChenchen.Models;

public partial class Enterprise
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    public string? BusinessName { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    public string? BusinessDescription { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    public string EconomicActivity { get; set; } = null!;

    public string Instagram { get; set; } = null!;

    [NotMapped]
    [Required(ErrorMessage = "Este campo es obligatorio")]
    public string? RucEmpresa { get; set; }


    [NotMapped]
    [Required(ErrorMessage = "Este campo es obligatorio")]
    public string? DvEmpresa { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    public string Ruc
    {
        get => $"{RucEmpresa} DV  {DvEmpresa}";
        set { }
    }

    public string WebSite { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public Guid RequestId { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    public string BusinessTime { get; set; } = null!;

    [Required(ErrorMessage = "Este campo es obligatorio")]
    public string Corregimiento { get; set; } = null!;

    [Required(ErrorMessage = "Este campo es obligatorio")]
    public string District { get; set; } = null!;

    [Required(ErrorMessage = "Este campo es obligatorio")]
    public decimal MonthlySales { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    public DateTime OperationsStartDate { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    public string Province { get; set; } = null!;

    [Required(ErrorMessage = "Este campo es obligatorio")]
    public decimal  ProyectedSales { get; set; }

    public virtual Request Request { get; set; } = null!;
}