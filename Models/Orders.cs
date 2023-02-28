namespace WebApplicationAPI.Models
{
    public class Orders
    {
        public Guid Id { get; set; }
        public string OrderId { get; set; }
        public string Product { get; set; }
        public string Customer { get; set; }
        public string Status { get; set; }
    public string Total { get; set; }
    public string Action { get; set; }

    }
}
