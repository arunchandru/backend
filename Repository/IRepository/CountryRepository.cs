using Microsoft.EntityFrameworkCore;
using WebApplicationAPI.Data;
using WebApplicationAPI.Models;

namespace WebApplicationAPI.Repository.IRepository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly PortalDbContext _DbContext;

        public CountryRepository(PortalDbContext portalDbContext)
        {
            _DbContext = portalDbContext;
        }
        public async Task AddCountry(Country entity)
        {
            await _DbContext.Country.AddAsync(entity);
            await Save();
        }

        public async Task DeleteCountry(Country entity)
        {
             _DbContext.Country.Remove(entity);
            await Save(); 
        }

        public async Task<List<Country>> GetCountry()
        {
            List<Country> countries = await _DbContext.Country.ToListAsync();
            return countries;
        }

        public async Task<Country> GetCountryById(int id)
        {
            var country = await _DbContext.Country.FindAsync(id);
            return country;
        }

        public bool isCountryExist(string name)
        {
            var result = _DbContext.Country.AsQueryable()
                .Where(x => x.Name.ToLower().Trim() == name.ToLower().Trim()).Any();
            return result;
        }

        public async Task Save()
        {
            await _DbContext.SaveChangesAsync();
        }

        public async Task UpdateCountry(Country entity)
        {
             _DbContext.Country.Update(entity);
            await Save();
        }
    }
}
