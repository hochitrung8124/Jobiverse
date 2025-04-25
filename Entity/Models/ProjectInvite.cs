using System;
using System.Collections.Generic;

namespace Entity.Models;

public partial class ProjectInvite
{
    public string InviteId { get; set; } = null!;

    public string? ProjectId { get; set; }

    public string? StudentId { get; set; }

    public string? Status { get; set; }

    public DateTime? InvitedAt { get; set; }

    public virtual Project? Project { get; set; }

    public virtual Student? Student { get; set; }
}
