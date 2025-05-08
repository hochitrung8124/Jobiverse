using System;

namespace API.DTOs;

public class AccLoginDto
{
  public string EmailOrPhone { get; set; } = null!;
  public string Password { get; set; } = null!;
}
