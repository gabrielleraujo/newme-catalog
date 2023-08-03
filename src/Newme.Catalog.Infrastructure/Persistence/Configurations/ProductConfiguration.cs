using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newme.Catalog.Domain.Entities;
using Newme.Catalog.Domain.Entities.Gender;

namespace Newme.Catalog.Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {            
            builder.ToTable("Products");
            builder.ConfigureEntity();

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .ValueGeneratedNever()
                .HasConversion(
                    x => x.value,
                    value => new ProductId(value)
                ).IsRequired();

            builder.Property(x => x.Stock)
                .HasColumnName("Stock")
                .IsRequired();

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasMaxLength(140)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnName("Description")
                .IsRequired();

            builder.Property(x => x.Sku)
                .HasColumnName("Sku")
                .HasConversion(
                    x => x.value,
                    value => new Sku(value)
                );

            builder.OwnsOne(x => x.Price, builder => {
                builder.Property(x => x.Currency)
                    .HasColumnName("Currency")
                    .HasMaxLength(3);

                builder.Property(x => x.Amount)
                    .HasColumnName("Amount");
            });

            builder.OwnsOne(x => x.DifferentialGroup, builder => {
                builder.Property(x => x.gender)
                    .ValueGeneratedNever()
                    .HasConversion(
                        x => x.value,
                        value => new GenderId(value)
                    )
                    .HasColumnName("GenderId")
                    .IsRequired();

                builder.Property(x => x.category)
                .ValueGeneratedNever()
                .HasConversion(
                    x => x.value,
                    value => new CategoryId(value)
                )
                .HasColumnName("CategoryId")
                .IsRequired();

                builder.Property(x => x.color)
                    .ValueGeneratedNever()
                    .HasConversion(
                        x => x.value,
                        value => new ColorId(value)
                    )
                    .HasColumnName("ColorId")
                    .IsRequired();;

                builder.Property(x => x.size)
                    .ValueGeneratedNever()
                    .HasConversion(
                        x => x.value,
                        value => new SizeId(value)
                    )
                    .HasColumnName("SizeId")
                    .IsRequired();
            });
        }
    }
} 