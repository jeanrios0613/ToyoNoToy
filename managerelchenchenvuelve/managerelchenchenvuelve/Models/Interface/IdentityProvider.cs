using System;
using System.Collections.Generic;

namespace managerelchenchenvuelve.Models.Interface;

public partial class IdentityProvider
{
    public int Id { get; set; }

    public string Scheme { get; set; } = null!;

    public string? DisplayName { get; set; }

    public bool Enabled { get; set; }

    public string Type { get; set; } = null!;

    public string? Properties { get; set; }

    public DateTime Created { get; set; }

    public DateTime? LastAccessed { get; set; }

    public bool NonEditable { get; set; }

    public DateTime? Updated { get; set; }
}
