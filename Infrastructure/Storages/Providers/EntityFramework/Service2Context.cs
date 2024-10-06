using Microsoft.EntityFrameworkCore;
using Storages.Providers.EntityFramework.Models;

namespace Storages.Providers.EntityFramework;

public sealed class Service2Context : DbContext
{
    public Service2Context(DbContextOptions<Service2Context> options) : base(options) => Database.EnsureCreated();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Supply>()
            .HasMany(e => e.Products)
            .WithOne()
            .HasForeignKey("SupplyGuid");
    }

    public DbSet<Manufacturer> Manufacturers { get; set; } = null!;

    public DbSet<Warehouse> Warehouses { get; set; } = null!;

    public DbSet<Product> Products { get; set; } = null!;

    public DbSet<Supply> Supplies { get; set; } = null!;
}