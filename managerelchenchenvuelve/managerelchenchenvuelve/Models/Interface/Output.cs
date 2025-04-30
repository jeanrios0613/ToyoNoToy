using System;
using System.Collections.Generic;

namespace managerelchenchenvuelve.Models.Interface;

public partial class Output
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid DataConnectorId { get; set; }

    public DateTimeOffset Created { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTimeOffset? Updated { get; set; }

    public string? UpdatedBy { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;

    public virtual DataConnector DataConnector { get; set; } = null!;
}
