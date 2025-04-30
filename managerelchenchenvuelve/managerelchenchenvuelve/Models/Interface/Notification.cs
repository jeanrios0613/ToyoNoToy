using System;
using System.Collections.Generic;

namespace managerelchenchenvuelve.Models.Interface;

public partial class Notification
{
    public Guid Id { get; set; }

    public string Subject { get; set; } = null!;

    public string? Body { get; set; }

    public string Receiver { get; set; } = null!;

    public string Sender { get; set; } = null!;

    public int SourceApp { get; set; }

    public DateTime CreationDate { get; set; }

    public int Type { get; set; }

    public bool IsRead { get; set; }

    public Guid GroupId { get; set; }

    public DateTime Created { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? Updated { get; set; }

    public string? UpdatedBy { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;

    public bool IsDelivered { get; set; }
}
