using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.DTOs;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JobiverseContext _context;
        private readonly IConfiguration _config;

        public AuthController(JobiverseContext jobiverseContext, IConfiguration config)
        {
            _config = config;
            _context = jobiverseContext;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest req)
        {
            var existingUser = await _context.Accounts
                .FirstOrDefaultAsync(u => u.Email == req.Email && u.Deleted == false);
            if (existingUser != null) return BadRequest(new { message = "Email already in use" });

            var newUser = new Account
            {
                AccountId = Guid.NewGuid().ToString(),
                Email = req.Email,
                PhoneNumber = req.PhoneNumber,
                Password = BCrypt.Net.BCrypt.HashPassword(req.Password),
                AccountType = req.AccountType,
                Deleted = false
            };

            _context.Accounts.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User registered successfully" });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(Account acc)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(u => u.Email == acc.Email && u.Deleted == false);
            if (account == null) return Unauthorized(new { message = "Invalid email" });

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(acc.Password);
            if (!BCrypt.Net.BCrypt.Verify(acc.Password, account.Password)) return Unauthorized(new { message = "Invalid password" });

            return Ok(new { token = "generated-jwt-token" });
        }
    }
}
