using WebApplicationAPI.Models;

namespace WebApplicationAPI.Repository.IRepository
{
    public interface ICountryRepository
    {
        Task<List<Country>> GetCountry();
        Task<Country> GetCountryById(int id);
        Task AddCountry(Country entity);
        Task UpdateCountry(Country entity);
        Task DeleteCountry(Country entity);
        Task Save();
        bool isCountryExist(string name);
    }
}
