using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XamarinAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace XamarinAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly AppDbContext _context;
        public DepartmentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<Department>>> GetDepts()
        {
            return await _context.Departments.ToListAsync();
        }
    }
}
