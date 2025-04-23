using System;
using System.Collections.Generic;

namespace managerelchenchenvuelve.Models;

public partial class TrayTheme
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string BackgroundColor { get; set; } = null!;

    public string TextColor { get; set; } = null!;

    public virtual ICollection<Tray> Trays { get; set; } = new List<Tray>();
}
