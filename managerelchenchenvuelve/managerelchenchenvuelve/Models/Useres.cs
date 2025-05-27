using System;
using System.Collections.Generic;

namespace managerelchenchenvuelve.Models;

public partial class Useres
{
    public string? Id { get; set; }

    public string? UserName { get; set; }

    public string? Email { get; set; }

    public string? PasswordHash { get; set; }

    public string? PhoneNumber { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public DateTime? Created { get; set; }

    public DateTime? Updatepass { get; set; }

    public string? Lastname { get; set; }

    public string? Names { get; set; }

    public bool Status { get; set; }
}
