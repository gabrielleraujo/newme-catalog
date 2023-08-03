using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newme.Catalog.Domain.Entities.Gender;

namespace Newme.Catalog.Infrastructure.Configurations
{
    public class GenderConfiguration : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.ToTable("Genders");
            builder.ConfigureDifferential();

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .ValueGeneratedNever()
                .HasConversion(
                    x => x.value,
                    value => new GenderId(value)
                )
                .HasDefaultValue()
                .IsRequired();

            builder.HasData(new List<Gender>(){
                new Gender(new GenderId(Guid.NewGuid()), "feminino"),
                new Gender(new GenderId(Guid.NewGuid()), "masculino"),
                new Gender(new GenderId(Guid.NewGuid()), "não se aplica")
            });
        }
    }
}