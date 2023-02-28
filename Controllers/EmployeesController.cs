using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationAPI.Data;
using WebApplicationAPI.Models;

namespace WebApplicationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly PortalDbContext _portalDbContext;

        public EmployeesController(PortalDbContext portalDbContext)
        {
            _portalDbContext = portalDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _portalDbContext.Employee.ToListAsync();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee( [FromBody] Employee empReq)
        {
           empReq.Id = Guid.NewGuid();
           await _portalDbContext.Employee.AddAsync(empReq);   
           await _portalDbContext.SaveChangesAsync();
           return Ok(empReq);
        }
    }
}
