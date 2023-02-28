using System.ComponentModel.DataAnnotations;

namespace WebApplicationAPI.Models
{
    public class Country
    { 
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string CountryCode { get; set; }

    }
}
