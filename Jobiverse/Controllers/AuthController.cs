using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.DTOs;
using System.Security.Claims;
using API.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JobiverseContext _context;
        private readonly JwtSettings _jwtSettings;

        public AuthController(JobiverseContext jobiverseContext, IOptions<JwtSettings> jwtOptions)
        {
            _context = jobiverseContext;
            _jwtSettings = jwtOptions.Value;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto req)
        {
            var email = req.Email.Trim();
            var phone = req.PhoneNumber.Trim();

            var existingAccount = await _context.Accounts
                .FirstOrDefaultAsync(u =>
                    (u.Email == email || u.PhoneNumber == phone)
                    && u.Deleted == false);

            if (existingAccount != null)
            {
                if (existingAccount.Email == email)
                    return BadRequest(new { message = "Email already in use" });
                if (existingAccount.PhoneNumber == phone)
                    return BadRequest(new { message = "Phone number already in use" });
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(req.Password);
            var newAccount = new Account
            {
                AccountId = Guid.NewGuid().ToString(),
                Email = email,
                PhoneNumber = phone,
                Password = hashedPassword,
                AccountType = req.AccountType
            };

            _context.Accounts.Add(newAccount);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User registered successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AccLoginDto req)
        {
            var account = await _context.Accounts
                .FirstOrDefaultAsync(u =>
                    (u.Email == req.EmailOrPhone || u.PhoneNumber == req.EmailOrPhone)
                    && u.Deleted == false);

            if (account == null) return Unauthorized(new { message = "Invalid email or phone number" });

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(req.Password, account.Password);
            if (!isPasswordValid) return Unauthorized(new { message = "Invalid password" });

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, account.Email),
                new Claim(ClaimTypes.NameIdentifier, account.AccountId ?? string.Empty),
                new Claim(ClaimTypes.Role, account.AccountType.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresInMinutes),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddMinutes(_jwtSettings.ExpiresInMinutes)
            });

            return Ok(new { token = "Login successfully" });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok(new { message = "Logout successfully" });
        }
    }
}
