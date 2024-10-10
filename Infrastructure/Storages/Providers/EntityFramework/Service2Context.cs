using Microsoft.EntityFrameworkCore;
using Storages.Providers.EntityFramework.Models;

namespace Storages.Providers.EntityFramework;

public sealed class Service2Context : DbContext
{
    public Service2Context(DbContextOptions<Service2Context> options) : base(options)
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Supply>()
            .HasMany<Product>()
            .WithOne()
            .IsRequired(false);

        modelBuilder.Entity<Manufacturer>()
            .HasMany<Product>()
            .WithOne()
            .HasForeignKey(p => p.ManufacturerGuid)
            .IsRequired();

        modelBuilder.Entity<Warehouse>()
            .HasMany<Product>()
            .WithOne()
            .HasForeignKey(p => p.WarehouseGuid)
            .IsRequired();
    }

    public DbSet<Manufacturer> Manufacturers { get; set; } = null!;

    public DbSet<Warehouse> Warehouses { get; set; } = null!;

    public DbSet<Product> Products { get; set; } = null!;

    public DbSet<Supply> Supplies { get; set; } = null!;
}
