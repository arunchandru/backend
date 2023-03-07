using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApplicationAPI.Data;
using WebApplicationAPI.DTO;
using WebApplicationAPI.Models;


namespace WebApplicationAPI.Controllers
{
  [ApiController]
  [EnableCors("MyPolicy")]
  [Route("api/[controller]")]
  public class FileManagerController : Controller
  {
    private readonly string AppDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
    private static  List<FileRecord> fileDb = new List<FileRecord>();
    private readonly PortalDbContext _portalDbContext;

    public FileManagerController(PortalDbContext portalDbContext)
    {
      _portalDbContext = portalDbContext;
    }

    [HttpPost]
    [Consumes("multipart/form-data")]  // annotate file type
    public async Task<HttpResponseMessage> PostAsync([FromForm] FileModelDto req)
    {
      FileRecord file = await SaveFileAsync(req.MyFile);
      if (!string.IsNullOrEmpty(file.FilePath))
      {
        file.AltText = req.AltText;
        file.Description = req.Description;
        await _portalDbContext.Document.AddAsync(file);
        await _portalDbContext.SaveChangesAsync();
        return new HttpResponseMessage(HttpStatusCode.OK);
      }
      else
        return new HttpResponseMessage(HttpStatusCode.BadRequest);
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
        fileRecord.Id = fileDb.Count + 1;
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
