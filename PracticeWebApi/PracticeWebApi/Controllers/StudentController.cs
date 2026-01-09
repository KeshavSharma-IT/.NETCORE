using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticeWebApi.Data;
using PracticeWebApi.Models;

namespace PracticeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentsDBContext _context;

        public StudentController(StudentsDBContext studentsDBContext)
        {
            _context = studentsDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get() {
            var students = await _context.Students.ToListAsync();

            return Ok(students);

        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var student = await _context.Students.FindAsync(Id);
            if (student == null) return NotFound();
            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student obj)
        {
            await _context.Students.AddAsync(obj);
            await _context.SaveChangesAsync();
            return Ok(obj);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Student obj)
        {
            if (id != obj.Id) return BadRequest();

            _context.Students.Update(obj);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int Id)
        {
            var student = await _context.Students.FindAsync(Id);

            if (student == null) return BadRequest();
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
