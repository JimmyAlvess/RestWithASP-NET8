using Microsoft.EntityFrameworkCore;
using RestWithASPNET.Models;

namespace RestWithASPNETErudio.Model.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
            
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}
