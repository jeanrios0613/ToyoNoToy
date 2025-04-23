using System;
using System.Collections.Generic;

namespace ElChenVuelveDashb.Models;

public partial class FormDefinition
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public Guid OwnerId { get; set; }

    public int Version { get; set; }

    public DateTimeOffset CreatedOn { get; set; }

    public string Configuration { get; set; } = null!;

    public DateTimeOffset ModifiedOn { get; set; }
}
