using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using WebApplicationAPI.Data;
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
    [Route("GetHelpQuestion")]
    public async Task<IActionResult> GetHelpQuestion()
    {
      List<HelpCenter> helpCenterList = new List<HelpCenter>();
      SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("webapplicationapiConnectionString"));
      SqlCommand command = new SqlCommand("Select * from HelpCenter", connection);
      SqlDataAdapter adapter = new SqlDataAdapter(command);
      DataTable dt = new DataTable();
      adapter.Fill(dt);

      for (int i = 0; i < dt.Rows.Count; i++)
      {
        HelpCenter obj = new HelpCenter();
        obj.Id = int.Parse(dt.Rows[i]["ID"].ToString());
        obj.Question = dt.Rows[i]["Question"].ToString();
        obj.Description = dt.Rows[i]["Description"].ToString();
        obj.QuestionType = dt.Rows[i]["QuestionType"].ToString();

        helpCenterList.Add(obj);
      }
      return Ok(helpCenterList);
    }


    [HttpPost]
    [Route("AddHelpQuestion")]

    public async Task<IActionResult> AddHelpQuestion([FromBody] HelpCenter req)
    {
      HelpCenter helpCenter = new HelpCenter();
      helpCenter.Question = req.Question;
      helpCenter.QuestionType = req.QuestionType;
      helpCenter.Description = req.Description.Replace("'", "''").Trim();


      SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("webapplicationapiConnectionString"));
      var query = "INSERT INTO HelpCenter(Question, Description, QuestionType) VALUES('" + helpCenter.Question + "','" + helpCenter.Description + "','" + helpCenter.QuestionType + "')";
      SqlCommand command = new SqlCommand(query, connection);
      connection.Open();
      command.ExecuteNonQuery();
      connection.Close();

      return Ok(helpCenter);
    }
  }

}
