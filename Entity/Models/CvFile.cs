using System;
using System.Collections.Generic;

namespace Entity.Models;

public partial class CvFile
{
    public string FileId { get; set; } = null!;

    public string? Cvid { get; set; }

    public string? FileName { get; set; }

    public string? FileUrl { get; set; }

    public DateTime? UploadedAt { get; set; }

    public virtual Cv? Cv { get; set; }
}
