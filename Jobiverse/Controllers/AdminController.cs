using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private JobiverseContext _jobiverseContext;

        public AdminController(JobiverseContext jobiverseContext)
        {
            _jobiverseContext = jobiverseContext;
        }

        [HttpGet]
        public IActionResult GettAllAccount()
        {
            return Ok(_jobiverseContext.Accounts.ToList());
        }

        [HttpDelete("{accountId}")]
        public IActionResult DeleteAccount(string accountId)
        {
            try
            {
                var checkId = _jobiverseContext.Accounts.SingleOrDefault(tt => tt.AccountId.Equals(accountId));
                if (checkId == null) 
                {
                    return NoContent();
                }
                _jobiverseContext.Remove(checkId);
                _jobiverseContext.SaveChanges();    
                return Ok("Delete success!");
            } catch 
            {
                return BadRequest();
            }
        }

        ///xem cac project
        ///
    }
}
