using Microsoft.EntityFrameworkCore;
using WebApplicationObras.Models;

namespace WebApplicationObras.Context
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base (options)
        {
            
        }

 

        public DbSet<Users> Users { get; set; }
    }
}
