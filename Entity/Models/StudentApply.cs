using System;
using System.Collections.Generic;

namespace Entity.Models;

public partial class StudentApply
{
    public int StudentApplyId { get; set; }

    public string? ProjectId { get; set; }

    public string? StudentId { get; set; }

    public string? Status { get; set; }

    public DateTime? AppliedAt { get; set; }

    public virtual Project? Project { get; set; }

    public virtual Student? Student { get; set; }
}
