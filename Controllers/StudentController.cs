using Backend.Business_Logic_Layer.DTO;
using Backend.Business_Logic_Layer.Services;
using Backend.Data_Access_Layer.Models;
using Business_Logic_Layer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentService _studentService;

        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }

        // ================= CREATE =================
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddStudent([FromForm] CreateStudentDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var student = await _studentService.AddStudentAsync(dto);
                return Ok(student);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // ================= GET ALL =================
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }

        // ================= GET BY ID =================
        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> GetStudentById(string id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
                return NotFound();
            return Ok(student);
        }

        // ================= UPDATE =================
        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateStudent(string id, [FromForm] UpdateStudentDTO dto)
        {
            try
            {
                await _studentService.UpdateStudentAsync(id, dto);
                return Ok(new { message = "updatedStudent" });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // ================= DELETE =================
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteStudent(string id)
        {
            try
            {
                await _studentService.DeleteStudentAsync(id);
                return Ok(new { message = "Student deleted successfully" });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
