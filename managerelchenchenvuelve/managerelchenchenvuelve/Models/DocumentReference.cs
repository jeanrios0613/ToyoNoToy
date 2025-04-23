using System;
using System.Collections.Generic;

namespace managerelchenchenvuelve.Models;

public partial class DocumentReference
{
    public Guid Id { get; set; }

    public Guid ProcessInstanceId { get; set; }

    public int? DocumentHandle { get; set; }

    public string? DocumentTitle { get; set; }

    public string? StageName { get; set; }

    public bool IsDeleted { get; set; }

    public string? RequiredName { get; set; }

    public int? ReferenceType { get; set; }

    public Guid? DocumentPdrId { get; set; }

    public int? SourceType { get; set; }

    public Guid? UserId { get; set; }

    public virtual ProcessInstance ProcessInstance { get; set; } = null!;
}
