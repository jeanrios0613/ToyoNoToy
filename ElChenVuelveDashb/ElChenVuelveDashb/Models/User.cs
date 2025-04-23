using System;
using System.Collections.Generic;

namespace ElChenVuelveDashb.Models;

public partial class User
{
    public Guid Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public DateTimeOffset Created { get; set; }

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();

    public virtual ICollection<Secret> Secrets { get; set; } = new List<Secret>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
