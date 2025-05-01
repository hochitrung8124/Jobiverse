using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private JobiverseContext _jobiverseContext;

        public ProjectController(JobiverseContext jobiverseContext)
        {
            _jobiverseContext = jobiverseContext;
        }

        [HttpGet]
        public IActionResult GettAll()
        {
            return Ok(_jobiverseContext.Projects.ToList());
        }
    }
}
