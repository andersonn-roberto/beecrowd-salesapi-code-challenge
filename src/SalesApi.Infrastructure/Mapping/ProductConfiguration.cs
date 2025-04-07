using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesApi.Domain.Models;

namespace SalesApi.Infrastructure.Mapping;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");
        builder.Property(p => p.Description).IsRequired().HasMaxLength(300);
        builder.Property(p => p.Category).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Image).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Price).IsRequired().HasPrecision(10, 2);
    }
}
