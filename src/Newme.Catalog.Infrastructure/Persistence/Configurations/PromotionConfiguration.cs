using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newme.Catalog.Domain.Entities;

namespace Newme.Catalog.Infrastructure.Configurations
{
    public class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
    {
        public void Configure(EntityTypeBuilder<Promotion> builder)
        {
            builder.ToTable("Promotions");
            builder.ConfigureEntity();

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .ValueGeneratedNever()
                .HasConversion(
                    x => x.value,
                    value => new PromotionId(value)
                ).IsRequired();

            builder.Property(x => x.ProductId)
                .HasColumnName("ProductId")
                .ValueGeneratedNever()
                .HasConversion(
                    x => x.value,
                    value => new ProductId(value)
                ).IsRequired();

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnName("Description")
                .IsRequired();

            builder.OwnsOne(x => x.Period, builder => {
                builder.Property(x => x.start)
                    .HasColumnName("Start")
                    .HasColumnType("datetime")
                    .IsRequired();

                builder.Property(x => x.end)
                    .HasColumnName("End")
                    .HasColumnType("datetime")
                    .IsRequired();
            });


            builder.OwnsOne(x => x.Discount, builder => {
                builder.Property(x => x.Percentage)
                .HasColumnName("Percentage")
                .IsRequired();

                builder.OwnsOne(y => y.Price, builder => {
                    builder.Property(y => y.Currency)
                        .HasColumnName("Currency")
                        .HasMaxLength(3)
                        .IsRequired();

                    builder.Property(y => y.Amount)
                        .HasColumnName("Amount")
                        .IsRequired();
                });
            });
        }
    }
}