using System;
using System.Collections.Generic;

namespace managerelchenchenvuelve.Models;

public partial class ActivityStateDefaultTray
{
    public int State { get; set; }

    public Guid TrayId { get; set; }

    public virtual ICollection<ActivityInstance> ActivityInstances { get; set; } = new List<ActivityInstance>();

    public virtual Tray Tray { get; set; } = null!;
}
