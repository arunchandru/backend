namespace WebApplicationAPI.Models
{
  public class Notification
  {
    public Guid Id { get; set; }
    public string Image { get; set; }
    public string Badge { get; set; }
    public string Title { get; set; }
    public string ActiveStatus { get; set; }
    public string Name { get; set; }
    public string Content { get; set; }

  }
}
