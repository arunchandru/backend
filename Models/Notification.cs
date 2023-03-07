using System.ComponentModel.DataAnnotations;

namespace WebApplicationAPI.Models
{
  public class Notification
  {
    [Key]
    public int Id { get; set; }

    public int FileId { get; set; }
    public string Badge { get; set; }
    public string Title { get; set; }
    public string ActiveStatus { get; set; }
    public string Name { get; set; }
    public string Content { get; set; }

  }
}
