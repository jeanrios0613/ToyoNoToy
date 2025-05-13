using System;
using System.Collections.Generic;

namespace managerelchenchenvuelve.Models;

public class Comments
{
    public int Id { get; set; }
    public string ProcessInstanceId { get; set; }
    public string Message { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string StageName { get; set; }


}
