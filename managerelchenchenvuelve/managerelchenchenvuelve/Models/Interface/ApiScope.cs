using System;
using System.Collections.Generic;

namespace managerelchenchenvuelve.Models.Interface;

public partial class ApiScope
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? DisplayName { get; set; }

    public string? Description { get; set; }

    public bool Required { get; set; }

    public bool Emphasize { get; set; }

    public bool ShowInDiscoveryDocument { get; set; }

    public bool Enabled { get; set; }

    public DateTime Created { get; set; }

    public DateTime? LastAccessed { get; set; }

    public bool NonEditable { get; set; }

    public DateTime? Updated { get; set; }

    public virtual ICollection<ApiScopeClaim> ApiScopeClaims { get; set; } = new List<ApiScopeClaim>();

    public virtual ICollection<ApiScopeProperty> ApiScopeProperties { get; set; } = new List<ApiScopeProperty>();
}
