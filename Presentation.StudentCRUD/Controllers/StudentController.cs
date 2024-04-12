using Application.StudentCRUD;
using Domain.StudentCRUD;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.StudentCRUD.Controllers
{
    public class StudentController : Controller
    {

        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost, Route("AddStudent")]
        public async Task<IActionResult> AddStudent(Student student)
        {
            var addStudent = await _studentService.AddStudent(student);
            return Ok(addStudent);
        }

        [HttpGet, Route("GetStudentById")]
        public async Task<IActionResult> GetStudentById(String id)
        {
            var Student = await _studentService.GetStudentById(id);
            return Ok(Student);
        }

        [HttpGet, Route("GetAllStudent")]
        public async Task<IActionResult> GetAllStudent()
        {
            var Student = await _studentService.GetAllStudent();
            return Ok(Student);
        }


        [HttpDelete, Route("DeleteStudent")]
        public async Task<IActionResult> DeleteStudent(String id)
        {
            await _studentService.DeleteStudent(id);
            return Ok();
        }

        [HttpPut, Route("UpdateStudent")]
        public async Task<IActionResult> UpdateStudent(Student student)
        {
            await _studentService.UpdateStudent(student);
            return StatusCode(StatusCodes.Status200OK, "Successfully updated");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
