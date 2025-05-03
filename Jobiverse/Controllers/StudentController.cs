using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private JobiverseContext _jobiverseContext;

        public StudentController(JobiverseContext jobiverseContext)
        {
            _jobiverseContext = jobiverseContext;
        }

        //Get/api/student
        //admin
        [HttpGet]
        public IActionResult GettAll()
        {
            return Ok(_jobiverseContext.Students.ToList());
        }

        //Get/api/student/{studentId}
        //admin
        [HttpGet("{studentId}")]
        public IActionResult GettStudentById(string studentId)
        {
            try
            {
                var checkId = _jobiverseContext.Students.SingleOrDefault(tt => tt.StudentId.Equals(studentId));
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

        //Get/api/student/by-name
        // select student by MSSV/ student, employer
        [HttpGet("by-name")]
        public IActionResult GetStudentByMSSV(string mssv)
        {
            try
            {
                //var student = _jobiverseContext.Students.SingleOrDefault(st => st.Mssv.Equals(mssv));
                var student = _jobiverseContext.Students
                .Where(st => st.Mssv == mssv)
                .Select(st => new {
                    st.Mssv,
                    st.Name,
                    st.Major.MajorName,
                    st.AvatarUrl,
                    st.University,                
                })
                .SingleOrDefault();
                if (student == null)
                {
                    return BadRequest("Name is not null");
                }
                return Ok(student);
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpPost("{studentId}")]
        public IActionResult AddInfor(string studentId, Student student)
        {
            try
            {
                var checkId = _jobiverseContext.Students.SingleOrDefault(tt => tt.StudentId.Equals(studentId));
                if (checkId == null)
                {
                    return BadRequest();
                }
                var _student = new Student
                {
                    Mssv = student.Mssv,
                    Name = student.Name,
                    University = student.University,
                    AvatarUrl = student.AvatarUrl,
                    MajorId = student.MajorId, // nhap vao ten chuyển thành id
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
