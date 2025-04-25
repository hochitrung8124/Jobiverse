using System;
using System.Collections.Generic;

namespace Entity.Models;

public partial class User
{
    public string AccountId { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string AccountType { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual Employer? Employer { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual Project? Project { get; set; }

    public virtual Student? Student { get; set; }
}
