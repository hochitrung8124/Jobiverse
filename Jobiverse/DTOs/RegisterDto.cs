using System;
using System.Text.Json.Serialization;

namespace API.DTOs;

public class RegisterDto
{
  public string Email { get; set; } = string.Empty;
  public string PhoneNumber { get; set; } = string.Empty;
  public string Password { get; set; } = string.Empty;
  public string AccountType { get; set; } = null!;
}
