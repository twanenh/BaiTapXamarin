using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XamarinAPI.DTO;
using XamarinAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace XamarinAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext _context;
        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetNhanViens()
        {
            var employees = _context.Employees
                .Include(e => e.Department)
                .Select(e => new EmployeeDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    Role = e.Role,
                    DepartmentId = e.DepartmentId,
                    DepartmentName = e.Department.Name
                }).ToList();

            return Ok(employees);

        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeDTO dto)
        {
            var employee = new Employee
            {
                Name = dto.Name,
                Role = dto.Role,
                DepartmentId = dto.DepartmentId
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return Ok(employee);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNhanVien(int id, EmployeeDTO nv)
        {
            if (id != nv.Id)
                return BadRequest();

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                return NotFound();

            employee.Name = nv.Name;
            employee.Role = nv.Role;
            employee.DepartmentId = nv.DepartmentId;

            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNhanVien(int id)
        {
            var nv = await _context.Employees.FindAsync(id);
            if (nv == null) return NotFound();
            _context.Employees.Remove(nv);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpGet("get-depts")]

        public async Task<ActionResult<IEnumerable<Department>>> GetDepts()
        {
            return await _context.Departments.ToListAsync();
        }
    }
}
