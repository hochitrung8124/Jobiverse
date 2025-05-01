using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Notification
{
    public string NotificationId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string? Content { get; set; }

    public sbyte? IsRead { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
