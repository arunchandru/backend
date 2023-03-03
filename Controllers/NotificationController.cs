using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationAPI.Data;
using WebApplicationAPI.Models;

namespace WebApplicationAPI.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class NotificationController : Controller
  {
    private readonly PortalDbContext _portalDbContext;

    public NotificationController(PortalDbContext portalDbContext)
    {
      _portalDbContext = portalDbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var notify = await _portalDbContext.Notification.ToListAsync();
      return Ok(notify);
    }
 
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Notification req)
    {
      req.Id = Guid.NewGuid();
      await _portalDbContext.Notification.AddAsync(req);
      await _portalDbContext.SaveChangesAsync();
      return Ok(req);
    }
  }


}
