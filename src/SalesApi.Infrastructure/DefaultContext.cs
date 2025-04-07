using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SalesApi.Domain.Models;

namespace SalesApi.Infrastructure;

public class DefaultContext(DbContextOptions<DefaultContext> options) : DbContext(options)
{
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Sale> Sales { get; set; }
    public virtual DbSet<SaleItem> SaleItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
