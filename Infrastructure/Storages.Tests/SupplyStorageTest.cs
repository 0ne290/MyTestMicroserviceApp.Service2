using Bogus;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Storages.Providers.EntityFramework;
using Storages.Providers.EntityFramework.Implementations;

namespace Storages.Tests;

public class SupplyStorageTest : IDisposable
{
    public SupplyStorageTest()
    {
        _dbContext = new Service2Context(new DbContextOptionsBuilder<Service2Context>()
            .UseSqlite("Data Source=./TestDb.db")
            .Options);
        _manufacturerStorage = new ManufacturerStorage(_dbContext);
        _warehouseStorage = new WarehouseStorage(_dbContext);
        _productStorage = new ProductStorage(_dbContext, _manufacturerStorage, _warehouseStorage);
        _supplyStorage = new SupplyStorage(_dbContext, _productStorage);
        
        _faker = new Faker("ru");
    }

    [Fact]
    public async Task Successful_Insert()
    {
        var product = (await _productStorage.GetAll()).First(p => p.Guid == "b10064b6-9a98-0830-41ae-57614c3b058c");
        var supply = new Supply(_faker.Random.Guid().ToString(),
            _faker.Date.Between(DateTime.Now.AddDays(-7), DateTime.Now.AddDays(7)),
            _faker.Random.Number(5000, 9999).ToString(),
            new Lazy<Task<IEnumerable<Product>>>(async () => await Task.FromResult(new[] { product })));

        await _supplyStorage.Insert(supply);
        
        Assert.True(true);
    }
    
    public void Dispose()
    {
        _dbContext.Dispose();
    }

    private readonly Service2Context _dbContext;

    private readonly IProductStorage _productStorage;

    private readonly IManufacturerStorage _manufacturerStorage;
    
    private readonly IWarehouseStorage _warehouseStorage;
    
    private readonly ISupplyStorage _supplyStorage;

    private readonly Faker _faker;
}