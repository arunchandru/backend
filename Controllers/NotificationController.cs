using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Net.Mail;
using WebApplicationAPI.Data;
using WebApplicationAPI.DTO;
using WebApplicationAPI.Models;

namespace WebApplicationAPI.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class NotificationController : Controller
  {
    private readonly PortalDbContext _portalDbContext;
    private readonly string AppDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
    private static List<FileRecord> fileDb = new List<FileRecord>();
    public NotificationController(PortalDbContext portalDbContext)
    {
      _portalDbContext = portalDbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      
      var notify = await _portalDbContext.Notification.ToListAsync();
      foreach (var item in notify)
      {
        var file = _portalDbContext.Document.Where(n => n.Id == item.Id).FirstOrDefault();

        var path = Path.Combine(AppDirectory, file?.FilePath);

        var memory = new MemoryStream();
        using (var stream = new FileStream(path, FileMode.Open))
        {
          await stream.CopyToAsync(memory);
        }

      }
        return Ok(notify);
    }
 
    [HttpPost]
    public async Task<IActionResult> Add([FromForm] NotificationFileModelDto req)
    {

      FileRecord file = await SaveFileAsync(req.MyFile);
      await _portalDbContext.Document.AddAsync(file);
      await _portalDbContext.SaveChangesAsync();

      Notification notify = new Notification();
      notify.FileId = file.Id;
      notify.Content = req.Content;
      notify.Title = req.Title;
      notify.ActiveStatus = req.ActiveStatus;
      notify.Badge = req.Badge;
      notify.Name = req.Name;


      await _portalDbContext.Notification.AddAsync(notify);
      await _portalDbContext.SaveChangesAsync();

      return Ok(notify);

    }

    private async Task<FileRecord> SaveFileAsync(IFormFile myFile)
    {
      FileRecord fileRecord = new FileRecord();
      if (myFile != null)
      {
        if (!Directory.Exists(AppDirectory))
        {
          Directory.CreateDirectory(AppDirectory);
        }
        var filename = DateTime.Now.Ticks.ToString() + Path.GetExtension(myFile.FileName);
        var filePath = Path.Combine(AppDirectory, filename);
        fileRecord.FileName = filename;
        fileRecord.FilePath = filePath;
        fileRecord.FileFormat = Path.GetExtension(fileRecord.FileName);
        fileRecord.ContentType = myFile.ContentType;
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
          await myFile.CopyToAsync(stream);
        }
        return fileRecord;
      }
      return fileRecord;
    }

  }


}
