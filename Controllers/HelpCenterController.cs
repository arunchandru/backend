using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using WebApplicationAPI.Models;

namespace WebApplicationAPI.Controllers
{
  [ApiController]
  public class HelpCenterController : Controller
  {
    private readonly IConfiguration _configuration;

    public HelpCenterController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    [Route("GetAllProduct")]
    public async Task<IActionResult> GelAllProduct()
    {
      List<HelpCenter> helpCenterList = new List<HelpCenter>();
      SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("webapplicationapiConnectionString"));
      SqlCommand command = new SqlCommand("Select * from HelpCenter", connection);
      SqlDataAdapter adapter = new SqlDataAdapter(command);
      DataTable dt = new DataTable();
      adapter.Fill(dt);

      for (int i = 0;i<dt.Rows.Count; i++)
      {
        HelpCenter obj = new HelpCenter();
        obj.Id = int.Parse(dt.Rows[i]["ID"].ToString());
        obj.Question= dt.Rows[i]["Question"].ToString();
        obj.Description = dt.Rows[i]["Description"].ToString();

        helpCenterList.Add(obj);
      }
      return View();
    }
  }
}
