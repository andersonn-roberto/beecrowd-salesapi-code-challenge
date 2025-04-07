using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesApi.Domain.Models;

namespace SalesApi.Infrastructure.Mapping;

public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
{
    public void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        builder.ToTable("SaleItems");
        builder.HasKey(si => si.Id);
        builder.Property(si => si.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");
        builder.Property(si => si.SaleId).IsRequired().HasColumnType("uuid");
        builder.Property(si => si.ProductId).IsRequired().HasColumnType("uuid");
        builder.Property(si => si.Quantity).IsRequired();
        builder.Property(si => si.UnitPrice).IsRequired().HasPrecision(10, 2);
        builder.Property(si => si.Discount).IsRequired().HasPrecision(10, 2);
        builder.Property(si => si.IsCancelled).IsRequired().HasDefaultValue(false);
        builder.Property(si => si.Total).HasPrecision(10, 2);
    }
}
