using Microsoft.EntityFrameworkCore;
using Ecommerce.Domain.Products;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infra.ORM.Mapping;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();

        builder.Property(p => p.Name)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(p => p.Price)
            .IsRequired()
            .HasPrecision(18, 2);
    }
}