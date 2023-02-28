using System.ComponentModel.DataAnnotations;

namespace WebApplicationAPI.DTO.country
{
    public class CreateCountryDto
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string CountryCode { get; set; }
    }
}
