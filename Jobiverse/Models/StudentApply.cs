using System;
using System.Collections.Generic;

namespace API.Models;

public partial class StudentApply
{
    public string StudentApplyId { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string StudentId { get; set; } = null!;

    public string? Status { get; set; }

    public DateTime? AppliedAt { get; set; }

    public bool? Deleted { get; set; }

    public virtual Project Project { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
