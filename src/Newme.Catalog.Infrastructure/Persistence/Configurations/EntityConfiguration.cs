using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newme.Catalog.Domain.Entities;

namespace Newme.Catalog.Infrastructure.Configurations
{
    public static class EntityConfiguration {
        public static void ConfigureEntity<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : Entity
        {
            builder.UseTpcMappingStrategy();

            builder.Property(x => x.Active)
                .HasColumnName("Active")
                .IsRequired();

            builder.Property(x => x.CreateDate)
                .HasColumnName("CreateDate")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(x => x.LastUpdateDate)
                .HasColumnName("LastUpdateDate")
                .HasColumnType("datetime")
                .IsRequired(false);
        }
    }
}