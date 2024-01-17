using AnimalZoo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AnimalZoo.Database
{
    public class ZooDbContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Enclosure> Enclosures { get; set; }

        public ZooDbContext(DbContextOptions<ZooDbContext> options) : base(options)
        {
        }


    }
}
