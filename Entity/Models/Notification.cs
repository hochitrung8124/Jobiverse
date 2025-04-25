using System;
using System.Collections.Generic;

namespace Entity.Models;

public partial class Notification
{
    public int NotificationId { get; set; }

    public string? UserId { get; set; }

    public string? Content { get; set; }

    public sbyte? IsRead { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User? User { get; set; }
}
