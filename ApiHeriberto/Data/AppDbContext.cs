using ApiHeriberto.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ApiHeriberto.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }

        public DbSet<Walk> Walks { get; set; }
        public DbSet<Image> Images { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var difficulties = new List<Difficulty>
            {
                new Difficulty {Id = Guid.Parse("fb4f8135-22e7-46e8-b78a-e78ac95c2763"), Name = "Easy" },
                new Difficulty { Id = Guid.Parse("43799e76-f436-4dd7-918b-0277fe6c74d9"), Name = "Medium" },
                new Difficulty { Id = Guid.Parse("3c9a813a-2c2c-4f91-a28f-8d0483ed935f"), Name = "Hard" },
            };
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

        }
    }
}
