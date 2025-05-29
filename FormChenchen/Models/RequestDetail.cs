using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FormChenchen.Models;

public partial class RequestDetail
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    public decimal QuantityToInvert { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    public string ReasonForMoney { get; set; } = null!;

    public Guid RequestId { get; set; }

    public DateTime CreationDate { get; set; }


    public string? VerifyClient { get; set; }

    public string? ManagementExecuted { get; set; }

    public string? TipoAtencion { get; set; }

    public string? ContactReason { get; set; }

    public virtual Request Request { get; set; } = null!;
}
