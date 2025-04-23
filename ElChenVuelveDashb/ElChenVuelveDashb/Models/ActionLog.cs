using System;
using System.Collections.Generic;

namespace ElChenVuelveDashb.Models;

public partial class ActionLog
{
    public Guid Id { get; set; }

    public Guid? UserId { get; set; }

    public DateTimeOffset CreatedDate { get; set; }

    public int ActionType { get; set; }

    public Guid? ProcessInstanceId { get; set; }

    public string ActionLogData { get; set; } = null!;

    public virtual ProcessInstance? ProcessInstance { get; set; }
}
