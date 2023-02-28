using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using WebApplicationAPI.Data;
using WebApplicationAPI.DTO.country;
using WebApplicationAPI.Models;
using WebApplicationAPI.Repository.IRepository;

namespace WebApplicationAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CountryController : Controller
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        public CountryController(ICountryRepository iCountryRepository, IMapper mapper) {
            _countryRepository = iCountryRepository;
            _mapper = mapper;
        }

        [HttpGet]

        public async Task<ActionResult<GetCountryDto>> GetCountry()
        {
            var country = await _countryRepository.GetCountry();
            if (country == null ) return NoContent();
            //var countryDto = _mapper.Map<GetCountryDto>(country);
            return Ok(country);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(200)]  // documentation the response status
        [ProducesResponseType(204)]
        public async Task<ActionResult<Country>> GetCountryById(int id) 
        {
            var country = await _countryRepository.GetCountryById(id);
            if (country == null) return NoContent();
            return Ok(country);
        }

        [HttpPost]
        [ProducesResponseType(201)] // status created
        [ProducesResponseType(409)] // conflict response
        public async Task<ActionResult<CreateCountryDto>> AddCountry([FromBody] CreateCountryDto  countryDto)
        {
            var isNameExist = _countryRepository.isCountryExist(countryDto.Name);
            if(isNameExist)   return Conflict("Country already exist");
            var country = _mapper.Map<Country>(countryDto);
            await _countryRepository.AddCountry(country);
            return CreatedAtAction("GetCountryById", new { id = country.Id },country); 
        }

        [HttpPut]
        public async Task<ActionResult<GetCountryDto>> updateCountry(int id, [FromBody] GetCountryDto country)
        {
            if (country == null && id == null) return BadRequest();
            var countryFromDb = await _countryRepository.GetCountryById(id);
            if (countryFromDb == null) return NotFound();
            var mappedcountry = _mapper.Map<Country>(country);
            await _countryRepository.UpdateCountry(mappedcountry);    
            return NoContent();
        }

        [HttpDelete("{id:int}")] 
        public async Task<ActionResult<Country>> DeleteCountry(int id)
        {   if(id == 0) return BadRequest();
            if(_countryRepository.GetCountryById(id) == null) return NotFound();
            var query = await _countryRepository.GetCountryById(id);
            await _countryRepository.DeleteCountry(query);
            return NoContent();
        }
        
        
    }
}
