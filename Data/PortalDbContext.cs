using Microsoft.EntityFrameworkCore;
using WebApplicationAPI.Models;

namespace WebApplicationAPI.Data
{
    public class PortalDbContext : DbContext //inherit from dbcontext in .core
    {
        //constructor
        public PortalDbContext(DbContextOptions<PortalDbContext> options) : base(options) //send options parent dbcontext class
        {

        }

        //configure table 
        public DbSet<Employee> Employee { get; set; }
        public DbSet <Orders> Orders { get; set; }
        public DbSet<Country> Country { get; set; }

    }
}
