using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 


namespace elchenchenvuelvecy.Models;

public partial class Enterprise
{
    public Guid Id { get; set; }

    public string? BusinessName { get; set; }

    public string? BusinessDescription { get; set; }

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

    public string BusinessTime { get; set; } = null!;

    public string Corregimiento { get; set; } = null!;

    public string District { get; set; } = null!;

    public decimal MonthlySales { get; set; }

    public DateTime OperationsStartDate { get; set; }

    public string Province { get; set; } = null!;

    public decimal ProyectedSales { get; set; }

    public virtual Request Request { get; set; } = null!;
}
