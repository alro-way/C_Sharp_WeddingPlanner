using Microsoft.EntityFrameworkCore;
 
namespace C_Sharp_WeddingPlanner.Models
{
    public class MyContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public MyContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users {get;set;}
        public DbSet<Login> Logins {get;set;}
        public DbSet<Wedding> Weddings {get;set;}
        public DbSet<Association> Associations {get;set;}
    }
}