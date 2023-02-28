//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Diagnostics.Metrics;
//using WebApplicationAPI.Data;
//using WebApplicationAPI.DTO.country;
//using WebApplicationAPI.Models;
//using WebApplicationAPI.Repository.IRepository;

//namespace WebApplicationAPI.Controllers
//{
//    [ApiController]
//    [Route("api/[Controller]")]
//    public class CountryController : Controller
//    {
//        private PortalDbContext _DbContext;
//        //private readonly ICountryRepository _countryRepository;
//        private readonly IMapper _mapper;
//        public CountryController(PortalDbContext portalDbContext, IMapper mapper) {
//            _DbContext = portalDbContext;
//            _mapper = mapper;
//        }

//        [HttpGet]

//        public ActionResult <GetCountryDto> GetCountry()
//        {
//            var country =  _DbContext.Country.ToList();
//            if (country == null )
//            {
//               return NoContent();
//            }
//            var countryDto = _mapper.Map<GetCountryDto>(country);
//            return Ok(countryDto);
//        }

//        [HttpGet("{id:int}")]
//        [ProducesResponseType(200)]  // documentation the response status
//        [ProducesResponseType(204)]
//        public ActionResult<Country> GetCountryById(int id) 
//        {
//            var country = _DbContext.Country.Find(id);
//            if (country == null)
//            {
//                return NoContent();
//            }
//            return Ok(country);
//        }

//        [HttpPost]
//        [ProducesResponseType(201)] // status created
//        [ProducesResponseType(409)] // conflict response
//        public  ActionResult<CreateCountryDto> AddCountry([FromBody] CreateCountryDto  countryDto)
//        {
//            var isNameExist = _DbContext.Country.AsQueryable().Where(x => x.Name.ToLower().Trim() == countryDto.Name.ToLower().Trim()).Any();
//            if(isNameExist) //validation
//            {
//                return Conflict("Country already exist");
//            }
//            //Country country = new Country(); // create empty object
//            //country.Name = countryDto.Name;
//            //country.ShortName = countryDto.ShortName;
//            //country.CountryCode = countryDto.CountryCode;

//            var country = _mapper.Map<Country>(countryDto); // using mapper do conversion in single line code

//            _DbContext.Country.Add(country);
//            _DbContext.SaveChanges();
//            return CreatedAtAction("GetCountryById", new { id = country.Id },country); //check weather data is created
//        }

//        [HttpPut]

//        public ActionResult<Country> updateCountry(int id, [FromBody] Country country)
//        {
//            if(country == null && id == null)
//            {
//                return BadRequest();
//            }
//            var countryFromDb = _DbContext.Country.Find(id);
//            if (countryFromDb == null)
//            {
//                return NotFound();
//            }
//            countryFromDb.Name = country.Name;
//            countryFromDb.ShortName = country.ShortName;
//            countryFromDb.CountryCode = country.CountryCode;

//            _DbContext.Country.Update(countryFromDb);
//            _DbContext.SaveChanges();
//            return Ok();
//        }

//        [HttpDelete("{id:int}")] 
//        public ActionResult<Country> DeleteCountry(int id)
//        {   if(id == 0) return BadRequest();
//            if(_DbContext.Country.Find(id) == null) return NotFound();
//            var query = _DbContext.Country.Find(id);
//            _DbContext.Country.Remove(query);
//            _DbContext.SaveChanges();
//            return Ok();
//        }
        
        
//    }
//}
