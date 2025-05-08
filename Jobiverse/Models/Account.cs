using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API.Models;

public partial class Account
{
    [JsonPropertyName("id")]
    public string? AccountId { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string AccountType { get; set; } = null!;

    public bool? Deleted { get; set; }

    public virtual Employer? Employer { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual Project? Project { get; set; }

    public virtual Student? Student { get; set; }
}
