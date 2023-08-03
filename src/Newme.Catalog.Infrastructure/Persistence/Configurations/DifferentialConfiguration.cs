using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newme.Catalog.Domain.Entities;

namespace Newme.Catalog.Infrastructure.Configurations
{
    public static class DifferentialConfiguration {
        public static void ConfigureDifferential<TDifferential>(this EntityTypeBuilder<TDifferential> builder) where TDifferential : Differential
        {
            builder.ConfigureEntity();
            builder.UseTpcMappingStrategy();

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .HasMaxLength(60)
                .IsRequired();
        }
    }
}