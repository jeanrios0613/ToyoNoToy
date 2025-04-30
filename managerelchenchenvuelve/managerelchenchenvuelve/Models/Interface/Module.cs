using System;
using System.Collections.Generic;

namespace managerelchenchenvuelve.Models.Interface;

public partial class Module
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTimeOffset Created { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTimeOffset? Updated { get; set; }

    public string? UpdatedBy { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;

    public string ClientId { get; set; } = null!;

    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}
