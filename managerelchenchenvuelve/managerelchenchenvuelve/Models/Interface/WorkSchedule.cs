using System;
using System.Collections.Generic;

namespace managerelchenchenvuelve.Models.Interface;

public partial class WorkSchedule
{
    public Guid Id { get; set; }

    public int Workday { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public bool IsCustom { get; set; }

    public DateTimeOffset Created { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTimeOffset? Updated { get; set; }

    public string? UpdatedBy { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;

    public bool IsWorkingDay { get; set; }
}
