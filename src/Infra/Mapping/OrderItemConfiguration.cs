using Ecommerce.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infra.ORM.Mapping;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItem");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();

        builder.OwnsOne(o => o.Product, product =>
        {
            product.Property(p => p.Id)
                .HasColumnName("ProductId")
                .IsRequired();

            product.Property(p => p.Name)
                .HasColumnName("ProductName")
                .IsRequired()
                .HasMaxLength(200);
        });

        builder.Property(o => o.Quantity).IsRequired();

        builder.Property(o => o.UnitPrice)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(o => o.TotalPrice)
            .IsRequired()
            .HasPrecision(18, 2);
    }
}