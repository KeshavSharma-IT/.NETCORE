using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiCodeFirst.Data;
using WebApiCodeFirst.Models;

namespace WebApiCodeFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDbContext context;

        public EmployeeController(EmployeeDbContext _employeeDbContext)
        {
            context = _employeeDbContext;
        }


        [HttpGet]
        public async Task<IActionResult> GetEmployeeList()
        {
            var employee = await context.employees.ToListAsync();
            return Ok(employee);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await context.employees.FirstOrDefaultAsync(e => e.EmpId == id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(Employee employee) {

            if (!ModelState.IsValid) {
                return BadRequest("Model state is not  valid");
            }
            employee.EmpId = 0;
            try
            {
                await context.employees.AddAsync(employee);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Return a clear 500 with the DB error (consider logging in real app)
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            // Return 201 with location header and the created entity (serializable)
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.EmpId }, employee);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateEmployee(int Id, Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("model state is not valid");
            }

            var existing = await context.employees.FindAsync(Id);
            if (existing == null)
            {
                return NotFound();
            }
            // copy values from incoming model to the tracked entity
            context.Entry(existing).CurrentValues.SetValues(employee);
            await context.SaveChangesAsync();
            //return Ok(employee);
            return NoContent();
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(int id) {

            if(id<=0)
            {
                return BadRequest();
            }
            var employee=await context.employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            context.employees.Remove(employee);
            await context.SaveChangesAsync();
            return NoContent();
        } 



    }
}
