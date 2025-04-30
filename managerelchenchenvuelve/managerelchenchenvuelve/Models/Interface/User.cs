using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace managerelchenchenvuelve.Models.Interface;

public class User : IdentityUser
{
    public DateTime Created { get; set; }

    public Guid? DepartmentId { get; set; }

    public string Lastname { get; set; } = null!;

    public string Names { get; set; } = null!;

    public bool Status { get; set; }

    public string? Position { get; set; }

    public bool IsExternal { get; set; }

    public virtual ICollection<Avatar> Avatars { get; set; } = new List<Avatar>();

    public virtual Department? Department { get; set; }

    public virtual ICollection<PoluxLog> PoluxLogs { get; set; } = new List<PoluxLog>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();

    public virtual ICollection<UserClaim> UserClaims { get; set; } = new List<UserClaim>();

    public virtual ICollection<UserLogin> UserLogins { get; set; } = new List<UserLogin>();

    public virtual ICollection<UserToken> UserTokens { get; set; } = new List<UserToken>();

    public virtual ICollection<Role> RolesNavigation { get; set; } = new List<Role>();
}
