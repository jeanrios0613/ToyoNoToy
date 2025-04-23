using System;
using System.Collections.Generic;

namespace ElChenVuelveDashb.Models;

public partial class Secret
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int SecretType { get; set; }

    public Guid UserId { get; set; }

    public DateTimeOffset Created { get; set; }

    public DateTimeOffset Updated { get; set; }

    public DateTimeOffset Expiration { get; set; }

    public virtual ICollection<SecretLog> SecretLogs { get; set; } = new List<SecretLog>();

    public virtual User User { get; set; } = null!;
}
