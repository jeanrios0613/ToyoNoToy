using System;
using System.Collections.Generic;

namespace managerelchenchenvuelve.Models.Interface;

public partial class Holiday
{
    public Guid Id { get; set; }

    public DateTime Date { get; set; }

    public string Reason { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTimeOffset Created { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTimeOffset? Updated { get; set; }

    public string? UpdatedBy { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;
}
