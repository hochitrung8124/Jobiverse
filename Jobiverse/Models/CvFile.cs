using System;
using System.Collections.Generic;

namespace API.Models;

public partial class CvFile
{
    public string FileId { get; set; } = null!;

    public string Cvid { get; set; } = null!;

    public string? FileName { get; set; }

    public string? FileUrl { get; set; }

    public DateTime? UploadedAt { get; set; }

    public bool? Deleted { get; set; }

    public virtual Cv Cv { get; set; } = null!;
}
