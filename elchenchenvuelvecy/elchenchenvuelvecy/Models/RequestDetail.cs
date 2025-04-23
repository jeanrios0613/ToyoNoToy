using System;
using System.Collections.Generic;

namespace elchenchenvuelvecy.Models;

public partial class RequestDetail
{
    public Guid Id { get; set; }

    public decimal QuantityToInvert { get; set; }

    public string ReasonForMoney { get; set; } = null!;

    public Guid RequestId { get; set; }

    public DateTime CreationDate { get; set; }

    public virtual Request Request { get; set; } = null!;
}
