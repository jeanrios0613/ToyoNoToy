using System;
using System.Collections.Generic;

namespace managerelchenchenvuelve.Models;

public partial class SecretLog
{
    public Guid Id { get; set; }

    public Guid SecretId { get; set; }

    public Guid UserId { get; set; }

    public string CallerInfo { get; set; } = null!;

    public DateTimeOffset Created { get; set; }

    public virtual Secret Secret { get; set; } = null!;
}
