using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployerController : ControllerBase
    {
        private JobiverseContext _jobiverseContext;

        public EmployerController(JobiverseContext jobiverseContext)
        {
            _jobiverseContext = jobiverseContext;
        }

        [HttpGet]
        public IActionResult GettAll()
        {
            return Ok(_jobiverseContext.Employers.ToList());
        }

        [HttpGet("{employerId}")]
        public IActionResult GettEmployerById(string employerId)
        {
            try
            {
                var checkId = _jobiverseContext.Employers.SingleOrDefault(tt => tt.EmployerId.Equals(employerId));
                if (checkId == null)
                {
                    return NoContent();
                }
                return Ok(checkId);
            }
            catch
            {
                return BadRequest(404);
            }
        }

        [HttpPost("{employerId}")]
        public IActionResult AddInfor(string employerId, Employer employer)
        {
            try
            {
                var checkId = _jobiverseContext.Employers.SingleOrDefault(tt => tt.EmployerId.Equals(employerId));
                if (checkId == null)
                {
                    return BadRequest();
                }
                var _employer = new Employer
                {
                    BusinessScale = employer.BusinessScale,
                    CompanyName = employer.CompanyName,
                    RepresentativeName = employer.RepresentativeName,
                    Job = employer.Job,
                    Industry = employer.Industry,
                    CompanyInfo = employer.CompanyInfo,
                    Prove = employer.Prove,
                    AddressEmployers = employer.AddressEmployers,
                };
                _jobiverseContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("{employerId}")]
        public IActionResult UpdateInfor(string employerId, Employer employer)
        {
            try
            {
                var checkId = _jobiverseContext.Employers.SingleOrDefault(tt => tt.EmployerId.Equals(employerId));
                if (checkId == null)
                {
                    return BadRequest();
                }
                var _employer = new Employer
                {
                    BusinessScale = employer.BusinessScale,
                    CompanyName = employer.CompanyName,
                    RepresentativeName = employer.RepresentativeName,
                    Job = employer.Job,
                    Industry = employer.Industry,
                    CompanyInfo = employer.CompanyInfo,
                    Prove = employer.Prove,
                    AddressEmployers = employer.AddressEmployers,
                };
                _jobiverseContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
