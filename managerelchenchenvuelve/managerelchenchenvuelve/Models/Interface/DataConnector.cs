using System;
using System.Collections.Generic;

namespace managerelchenchenvuelve.Models.Interface;

public partial class DataConnector
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public int Type { get; set; }

    public string EntityName { get; set; } = null!;

    public string TriggerFieldName { get; set; } = null!;

    public DateTimeOffset Created { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTimeOffset? Updated { get; set; }

    public string? UpdatedBy { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;

    public string? SortBy { get; set; }

    public bool SortDescending { get; set; }

    public virtual ICollection<Output> Outputs { get; set; } = new List<Output>();
}
