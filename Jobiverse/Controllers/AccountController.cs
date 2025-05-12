using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JobiverseContext _context;

        public AccountController(JobiverseContext jobiverseContext)
        {
            _context = jobiverseContext;
        }

        [HttpGet("account")]
        [Authorize(Roles = "admin")]
        public IActionResult GetAllAccount()
        {
            var accounts = _context.Accounts.ToList();
            if (accounts == null || accounts.Count == 0)
            {
                return NotFound(new { message = "No accounts found" });
            }

            return Ok(accounts);
        }
    }
}
