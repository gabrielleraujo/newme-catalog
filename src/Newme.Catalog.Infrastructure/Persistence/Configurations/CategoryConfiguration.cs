using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newme.Catalog.Domain.Entities;

namespace Newme.Catalog.Infrastructure.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Caterories");
            builder.ConfigureDifferential();

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .ValueGeneratedNever()
                .HasConversion(
                    x => x.value,
                    value => new CategoryId(value)
                ).IsRequired();
        }
    }
}