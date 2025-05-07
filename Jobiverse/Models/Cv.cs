using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Cv
{
    public string Cvid { get; set; } = null!;

    public string StudentId { get; set; } = null!;

    public string? Title { get; set; }

    public string? Summary { get; set; }

    public string? Skills { get; set; }

    public string? Experience { get; set; }

    public string? Education { get; set; }

    public string? Certifications { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? Deleted { get; set; }

    public virtual ICollection<CvFile> CvFiles { get; set; } = new List<CvFile>();

    public virtual Student Student { get; set; } = null!;
}
