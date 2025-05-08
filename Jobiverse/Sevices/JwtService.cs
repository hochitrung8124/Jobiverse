using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Models;
using Microsoft.IdentityModel.Tokens;

namespace API.Sevices;

public class JwtService
{
  private readonly IConfiguration _config = null!;

  public JwtService(IConfiguration config)
  {
    _config = config;
  }

  public string GenerateToken(Account acc)
  {
    if (acc == null || acc.AccountId == null)
    {
      throw new ArgumentNullException(nameof(acc), "Account or AccountId cannot be null.");
    }

    var claims = new[] {
      new Claim(ClaimTypes.NameIdentifier, acc.AccountId.ToString()),
      new Claim(ClaimTypes.Email, acc.Email.ToString())
      // new Claim("id", acc.AccountId.ToString()),
      // new Claim("accountType", acc.Email.ToString()),
    };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
      expires: DateTime.UtcNow.AddDays(7),
      claims: claims,
      signingCredentials: creds
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
  }

  public ClaimsPrincipal? VerifyToken(string token)
  {
    var tokenHandler = new JwtSecurityTokenHandler();
    try
    {
      var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);
      var parameters = new TokenValidationParameters
      {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
      };

      var principal = tokenHandler.ValidateToken(token, parameters, out _);
      return principal;
    }
    catch (System.Exception)
    {
      return null;
      throw;
    }
  }
}
