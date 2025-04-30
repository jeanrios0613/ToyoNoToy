using System;
using System.Collections.Generic;

namespace managerelchenchenvuelve.Models.Interface;

public partial class PoluxLog
{
    public Guid Id { get; set; }

    public string UserId { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Ip { get; set; } = null!;

    public DateTimeOffset Created { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTimeOffset? Updated { get; set; }

    public string? UpdatedBy { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
