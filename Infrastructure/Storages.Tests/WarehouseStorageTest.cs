using Bogus;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Storages.Providers.EntityFramework;
using Storages.Providers.EntityFramework.Implementations;

namespace Storages.Tests;

public class WarehouseStorageTest : IDisposable
{
    public WarehouseStorageTest()
    {
        _dbContext = new Service2Context(new DbContextOptionsBuilder<Service2Context>().UseSqlite("Data Source=./TestDb.db")
            .Options);
        _warehouseStorage = new WarehouseStorage(_dbContext);
        _faker = new Faker("ru");
    }
    
    [Fact]
    public async Task Successful_Insert()
    {
        const int countOfTestWarehouses = 20;
        var testWarehouses = new Warehouse[countOfTestWarehouses];
        for (var i = 0; i < countOfTestWarehouses; i++)
        {
            testWarehouses[i] = new Warehouse(_faker.Random.Guid().ToString(), _faker.Address.FullAddress(),
                (_faker.Address.Longitude(), _faker.Address.Latitude()));
            await _warehouseStorage.Insert(testWarehouses[i]);
        }
        var warehousesFromStorage = await _warehouseStorage.GetAll();
        
        for (var i = 0; i < countOfTestWarehouses; i++)
            Assert.Contains(warehousesFromStorage, w => w.Equals(testWarehouses[i]));
    }
    
    public void Dispose()
    {
        _dbContext.Dispose();
    }

    private readonly Service2Context _dbContext;

    private readonly WarehouseStorage _warehouseStorage;

    private readonly Faker _faker;
}