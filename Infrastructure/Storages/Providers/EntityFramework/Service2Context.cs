using Bogus;
using Microsoft.EntityFrameworkCore;
using Storages.Providers.EntityFramework.Models;

namespace Storages.Providers.EntityFramework;

public sealed class Service2Context : DbContext
{
    public static async Task EnsureCreatedAndLoadTestData(Service2Context dbContext)
    {
        var faker = new Faker("ru");
        var numberOfWarehouses = faker.Random.Int(3, 8);
        var numberOfManufacturers = faker.Random.Int(15, 22);
        
        var warehouses = new List<Warehouse>(numberOfWarehouses);
        var manufacturers = new List<Manufacturer>(numberOfManufacturers);
        var products = new List<Product>(numberOfWarehouses * numberOfManufacturers * 15);
        for (var i = 0; i < numberOfWarehouses; i++)
            warehouses.Add(new Warehouse
            {
                Guid = faker.Random.Guid().ToString(), Address = faker.Address.FullAddress(),
                GeolocationLatitude = faker.Address.Latitude(), GeolocationLongitude = faker.Address.Longitude()
            });
        for (var i = 0; i < numberOfManufacturers; i++)
            manufacturers.Add(new Manufacturer
            {
                Guid = faker.Random.Guid().ToString(), Address = faker.Address.FullAddress(),
                Name = faker.Company.CompanyName()
            });
        foreach (var warehouse in warehouses)
            foreach (var manufacturer in manufacturers)
                for (var i = 0; i < faker.Random.Int(5, 15); i++)
                    products.Add(new Product
                    {
                        Guid = faker.Random.Guid().ToString(), ManufacturerGuid = manufacturer.Guid,
                        Name = faker.Commerce.ProductName(), ReceiptDate = DateTime.Now, SupplyGuid = null,
                        WarehouseGuid = warehouse.Guid
                    });

        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.EnsureCreatedAsync();

        await dbContext.Warehouses.AddRangeAsync(warehouses);
        await dbContext.Manufacturers.AddRangeAsync(manufacturers);
        await dbContext.Products.AddRangeAsync(products);
    }

    public Service2Context(DbContextOptions<Service2Context> options) : base(options) { }

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
