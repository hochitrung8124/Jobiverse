using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Major
{
    public string MajorId { get; set; } = null!;

    public string MajorName { get; set; } = null!;

    public bool? Deleted { get; set; }

    public virtual ICollection<Discipline> Disciplines { get; set; } = new List<Discipline>();

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
