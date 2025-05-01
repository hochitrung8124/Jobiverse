using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Student
{
    public string StudentId { get; set; } = null!;

    public string AccountId { get; set; } = null!;

    public string? Mssv { get; set; }

    public string? Name { get; set; }

    public string? MajorId { get; set; }

    public string? University { get; set; }

    public string? AvatarUrl { get; set; }

    public virtual User Account { get; set; } = null!;

    public virtual ICollection<Cv> Cvs { get; set; } = new List<Cv>();

    public virtual Major? Major { get; set; }

    public virtual ICollection<StudentApply> StudentApplies { get; set; } = new List<StudentApply>();
}
