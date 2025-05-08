using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.DTOs;
using API.Sevices;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JobiverseContext _context;
        private readonly IConfiguration _config;
        private readonly JwtService _jwtService;

        public AuthController(JobiverseContext jobiverseContext, IConfiguration config)
        {
            _config = config;
            _context = jobiverseContext;
            _jwtService = new JwtService(config);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AccRegisterDto req)
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

            var token = _jwtService.GenerateToken(account);
            Response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(7),
            });

            return Ok(new { token = "Login successfully" });
        }

        [HttpGet("me")]
        public IActionResult Me()
        {
            var accountName = User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var accountId = User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (accountName == null) return Unauthorized();
            return Ok(new { accountName, accountId });
        }
    }
}
