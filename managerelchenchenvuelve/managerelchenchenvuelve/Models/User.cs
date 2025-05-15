using System;
using System.Collections.Generic;

namespace managerelchenchenvuelve.Models;

public partial class User
{
    public Guid Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Name { get; set; }

    public string? Userss { get; set; }

    public string? Lastname { get; set; }

    public DateTimeOffset Created { get; set; }

	public string? Roles { get; set; } 

    public virtual ICollection<Secret> Secrets { get; set; } = new List<Secret>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
