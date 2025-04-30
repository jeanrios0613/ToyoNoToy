using System;
using System.Collections.Generic;

namespace managerelchenchenvuelve.Models.Interface;

public partial class Permission
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public Guid ModuleId { get; set; }

    public DateTimeOffset Created { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTimeOffset? Updated { get; set; }

    public string? UpdatedBy { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;

    public string Key { get; set; } = null!;

    public virtual Module Module { get; set; } = null!;

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
