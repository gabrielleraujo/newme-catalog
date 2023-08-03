using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newme.Catalog.Domain.Entities;

namespace Newme.Catalog.Infrastructure.Configurations
{
    public class ColorConfiguration : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder.ToTable("Colors");
            builder.ConfigureDifferential();
            
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .ValueGeneratedNever()
                .HasConversion(
                    x => x.value,
                    value => new ColorId(value)
                ).IsRequired();
        }
    }
}