using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesApi.Domain.Models;

namespace SalesApi.Infrastructure.Mapping;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");
        builder.Property(s => s.SaleNumber).IsRequired();
        builder.Property(s => s.SaleDate).IsRequired().HasColumnType("timestamp with time zone").HasDefaultValueSql("now()");
        builder.Property(s => s.CustomerId).IsRequired().HasColumnType("uuid");
        builder.Property(s => s.BranchId).IsRequired().HasColumnType("uuid");
        builder.Property(s => s.Cancelled).IsRequired().HasDefaultValue(false);
        builder.Property(s => s.TotalAmount).HasPrecision(10, 2);

        builder.HasMany(s => s.Items)
            .WithOne()
            .HasForeignKey(si => si.SaleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(s => s.SaleNumber).IsUnique();
    }
}
