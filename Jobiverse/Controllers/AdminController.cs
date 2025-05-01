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
        public IActionResult GettAll()
        {
            return Ok(_jobiverseContext.Users.ToList());
        }
    }
}
