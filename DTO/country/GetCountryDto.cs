using System.ComponentModel.DataAnnotations;

namespace WebApplicationAPI.DTO.country
{
    public class GetCountryDto
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string CountryCode { get; set; }
    }
}
