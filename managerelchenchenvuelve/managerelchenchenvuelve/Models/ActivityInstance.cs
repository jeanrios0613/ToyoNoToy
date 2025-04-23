using System;
using System.Collections.Generic;

namespace managerelchenchenvuelve.Models;

public partial class ActivityInstance
{
    public Guid Id { get; set; }

    public Guid InstanceId { get; set; }

    public int Order { get; set; }

    public string Name { get; set; } = null!;

    public string DefinitionKey { get; set; } = null!;

    public Guid FormId { get; set; }

    public int State { get; set; }

    public DateTimeOffset Created { get; set; }

    public DateTimeOffset MarkedAsPending { get; set; }

    public DateTimeOffset Completed { get; set; }

    public DateTimeOffset? MarkedAsFailure { get; set; }

    public Guid? ActorId { get; set; }

    public Guid? AssignTo { get; set; }

    public DateTimeOffset? DueDateOffSet { get; set; }

    public string? Description { get; set; }

    public int? Type { get; set; }

    public DateTime? DueDate { get; set; }

    public Guid? TrayId { get; set; }

    public DateTimeOffset Started { get; set; }

    public int RetriedCount { get; set; }

    public int RetryLimit { get; set; }

    public int DueDateType { get; set; }

    public virtual ICollection<ActivityInstanceRole> ActivityInstanceRoles { get; set; } = new List<ActivityInstanceRole>();

    public virtual ProcessInstance Instance { get; set; } = null!;

    public virtual ActivityStateDefaultTray StateNavigation { get; set; } = null!;

    public virtual Tray? Tray { get; set; }
}
