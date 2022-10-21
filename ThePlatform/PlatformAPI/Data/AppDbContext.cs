using Microsoft.EntityFrameworkCore;
using PlatformAPI.Models;

namespace PlatformAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        public DbSet<Platform> Platforms { get; set; }
    }
}
