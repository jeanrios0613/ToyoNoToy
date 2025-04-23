using System;
using System.Collections.Generic;

namespace ElChenVuelveDashb.Models;

public partial class Comment
{
    public Guid Id { get; set; }

    public Guid ProcessInstanceId { get; set; }

    public string Message { get; set; } = null!;

    public Guid CreatedBy { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public int CommentType { get; set; }

    public string StageName { get; set; } = null!;

    public virtual ProcessInstance ProcessInstance { get; set; } = null!;
}
