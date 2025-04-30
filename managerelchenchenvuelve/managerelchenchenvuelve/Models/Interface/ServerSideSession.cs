using System;
using System.Collections.Generic;

namespace managerelchenchenvuelve.Models.Interface;

public partial class ServerSideSession
{
    public int Id { get; set; }

    public string Key { get; set; } = null!;

    public string Scheme { get; set; } = null!;

    public string SubjectId { get; set; } = null!;

    public string? SessionId { get; set; }

    public string? DisplayName { get; set; }

    public DateTime Created { get; set; }

    public DateTime Renewed { get; set; }

    public DateTime? Expires { get; set; }

    public string Data { get; set; } = null!;
}
