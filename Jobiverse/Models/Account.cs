using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API.Models;

public partial class Account
{
    [JsonPropertyName("id")]
    public string? AccountId { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; } = null!;

    [JsonPropertyName("phone")]
    public string PhoneNumber { get; set; } = null!;

    [JsonPropertyName("password")]
    public string Password { get; set; } = null!;

    [JsonPropertyName("accountType")]
    public string AccountType { get; set; } = null!;

    [JsonPropertyName("deleted")]
    public bool? Deleted { get; set; }

    public virtual Employer? Employer { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual Project? Project { get; set; }

    public virtual Student? Student { get; set; }
}
