using System;
using System.Collections.Generic;

namespace managerelchenchenvuelve.Models.Interface;

public partial class Avatar
{
    public Guid Id { get; set; }

    public string UserId { get; set; } = null!;

    public string ImagePath { get; set; } = null!;

    public string ContentType { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
