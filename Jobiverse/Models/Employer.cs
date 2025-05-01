using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Employer
{
    public string EmployerId { get; set; } = null!;

    public string AccountId { get; set; } = null!;

    public string BusinessScale { get; set; } = null!;

    public string? CompanyName { get; set; }

    public string? RepresentativeName { get; set; }

    public string? Job { get; set; }

    public string? Industry { get; set; }

    public string? CompanyInfo { get; set; }

    public string? Prove { get; set; }

    public string? AddressEmployers { get; set; }

    public virtual User Account { get; set; } = null!;
}
