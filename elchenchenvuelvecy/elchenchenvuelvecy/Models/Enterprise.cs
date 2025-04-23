using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Azure.Core;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace elchenchenvuelvecy.Models;

public partial class Enterprise
{
    [Key]
    public Guid Id { get; set; }

    public string? BusinessName { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    public string? BusinessDescription { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    public string EconomicActivity { get; set; } = null!;

    public string Instagram { get; set; } = null!;
 
    [Required(ErrorMessage = "Este campo es obligatorio")]
    private string? RucEmpresa { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    private string? DvEmpresa { get; set; }

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

    public string Province { get; set; } = null!;

    [Required(ErrorMessage = "Este campo es obligatorio")]
    public decimal ProyectedSales { get; set; }

    public virtual Request Request { get; set; } = null!;
}
