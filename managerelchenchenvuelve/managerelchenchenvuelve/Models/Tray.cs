using System;
using System.Collections.Generic;

namespace managerelchenchenvuelve.Models;

public partial class Tray
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid ThemeId { get; set; }

    public virtual ICollection<ActivityInstance> ActivityInstances { get; set; } = new List<ActivityInstance>();

    public virtual ICollection<ActivityStateDefaultTray> ActivityStateDefaultTrays { get; set; } = new List<ActivityStateDefaultTray>();

    public virtual TrayTheme Theme { get; set; } = null!;
}
