using System;
using System.Collections.Generic;

namespace Entity.Models;

public partial class Project
{
    public string ProjectId { get; set; } = null!;

    public string AccountId { get; set; } = null!;

    public string? MajorId { get; set; }

    public string? DisciplineId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Content { get; set; }

    public string? WorkingTime { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User Account { get; set; } = null!;

    public virtual Discipline? Discipline { get; set; }

    public virtual Major? Major { get; set; }

    public virtual ICollection<ProjectInvite> ProjectInvites { get; set; } = new List<ProjectInvite>();

    public virtual ICollection<StudentApply> StudentApplies { get; set; } = new List<StudentApply>();
}
