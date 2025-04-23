using System;
using System.Collections.Generic;

namespace managerelchenchenvuelve.Models;

public partial class ProcessInstance
{
    public Guid Id { get; set; }

    public Guid DefinitionId { get; set; }

    public int CurrentStep { get; set; }

    public Guid UserId { get; set; }

    public string DataObject { get; set; } = null!;

    public int Status { get; set; }

    public DateTimeOffset Created { get; set; }

    public DateTimeOffset Updated { get; set; }

    public string? EndName { get; set; }

    public string Name { get; set; } = null!;

    public int Sequential { get; set; }

    public string TransformedSequential { get; set; } = null!;

    public virtual ICollection<ActionLog> ActionLogs { get; set; } = new List<ActionLog>();

    public virtual ICollection<ActivityInstance> ActivityInstances { get; set; } = new List<ActivityInstance>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ProcessDefinition Definition { get; set; } = null!;

    public virtual ICollection<DocumentReference> DocumentReferences { get; set; } = new List<DocumentReference>();
}
