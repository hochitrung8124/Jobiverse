using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Discipline
{
    public string DisciplineId { get; set; } = null!;

    public string? MajorId { get; set; }

    public string DisciplineName { get; set; } = null!;

    public bool? Deleted { get; set; }

    public virtual Major? Major { get; set; }
}
