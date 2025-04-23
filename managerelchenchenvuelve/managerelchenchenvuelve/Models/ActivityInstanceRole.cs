using System;
using System.Collections.Generic;

namespace managerelchenchenvuelve.Models;

public partial class ActivityInstanceRole
{
    public Guid Id { get; set; }

    public Guid ActivityInstanceId { get; set; }

    public Guid RoleId { get; set; }

    public virtual ActivityInstance ActivityInstance { get; set; } = null!;
}
