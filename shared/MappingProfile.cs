using AutoMapper;
using WebApplicationAPI.DTO.country;
using WebApplicationAPI.Models;

namespace WebApplicationAPI.shared
{
    public class MappingProfile: Profile
    {
           public MappingProfile() 
        {
            //Source, Destination
            CreateMap<CreateCountryDto, Country>().ReverseMap();
        }
    }
}
