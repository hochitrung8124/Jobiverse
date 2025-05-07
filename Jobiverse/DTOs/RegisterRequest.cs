using System;

namespace API.DTOs;

public class RegisterRequest
{
  public string Email { get; set; } = null!;
  public string Password { get; set; } = null!;
  public string PhoneNumber { get; set; } = null!;
  public string AccountType { get; set; } = null!;
}
