using Microsoft.EntityFrameworkCore;
using WebApplicationAPI.DTO;
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
        public DbSet <Orders> Orders { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<FileRecord> Document { get; set; }
        public DbSet<HelpCenter> HelpCenter { get; set; }


  }
}
