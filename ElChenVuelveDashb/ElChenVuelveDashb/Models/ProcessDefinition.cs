using System;
using System.Collections.Generic;

namespace ElChenVuelveDashb.Models;

public partial class ProcessDefinition
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid OwnerId { get; set; }

    public int InstanceCycleTime { get; set; }

    public int Version { get; set; }

    public DateTimeOffset CreatedOn { get; set; }

    public string Flow { get; set; } = null!;

    public string? Icon { get; set; }

    public virtual ProcessDefinitionSequential? ProcessDefinitionSequential { get; set; }

    public virtual ICollection<ProcessInstance> ProcessInstances { get; set; } = new List<ProcessInstance>();
}
