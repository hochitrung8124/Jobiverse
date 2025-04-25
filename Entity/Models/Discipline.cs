using System;
using System.Collections.Generic;

namespace Entity.Models;

public partial class Discipline
{
    public string DisciplineId { get; set; } = null!;

    public string? MajorId { get; set; }

    public string? DisciplineName { get; set; }

    public virtual Major? Major { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
