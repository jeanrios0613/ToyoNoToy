using System;
using System.Collections.Generic;

namespace managerelchenchenvuelve.Models;

public class Comment
{
    public int Id { get; set; }
    public string RequestCode { get; set; }
    public string CommentText { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
}
