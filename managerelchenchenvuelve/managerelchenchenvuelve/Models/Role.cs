using System;
using System.Collections.Generic;

namespace managerelchenchenvuelve.Models;

public partial class Role
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTimeOffset Created { get; set; }

    public DateTimeOffset Modified { get; set; }

    public Guid OwnerId { get; set; }

    public virtual User Owner { get; set; } = null!;

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
