using Microsoft.EntityFrameworkCore;
using Newme.Catalog.Domain.Entities;
using Newme.Catalog.Domain.Entities.Gender;
using Newme.Catalog.Infrastructure.Configurations;

namespace Newme.Catalog.Infrastructure.Persistence
{
    public class CatalogCommandContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Promotion> Promotion { get; set; }

        public CatalogCommandContext(DbContextOptions<CatalogCommandContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new GenderConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ColorConfiguration());
            modelBuilder.ApplyConfiguration(new SizeConfiguration());
            modelBuilder.ApplyConfiguration(new PromotionConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}