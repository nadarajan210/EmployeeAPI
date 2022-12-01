using EmployeeAPI.Data;
using EmployeeAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DataContext _db;
        public EmployeeController(DataContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetEmployees()
        {
            return Ok(await _db.Employees.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<Employee>>> CreateEmployee(Employee emp)
        {
            _db.Employees.Add(emp);
            await _db.SaveChangesAsync();
            return Ok(await _db.Employees.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Employee>>> UpdateEmployee(Employee emp)
        {
            var demp = await _db.Employees.FindAsync(emp.Id);
            if(demp == null)
            {
                return BadRequest("Employee Not Found.");
            }
            demp.Name = emp.Name;
            demp.Department = emp.Department;
            demp.Salary = emp.Salary;
            demp.City = emp.City;

            await _db.SaveChangesAsync();
            
            return Ok(await _db.Employees.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Employee>>> DeleteEmployee(int id)
        {
            var emp = await _db.Employees.FindAsync(id);
            if (emp == null)
            {
                return BadRequest("Employee not Found.");
            }
            _db.Employees.Remove(emp);
            await _db.SaveChangesAsync();

            return Ok(await _db.Employees.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmplyee(int id)
        {
            var emp = await _db.Employees.FindAsync(id);
            if (emp == null)
            {
                return BadRequest("Employee Not Found.");
            }

            return Ok(emp);
        }
    }
}
