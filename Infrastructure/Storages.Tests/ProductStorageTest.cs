using Bogus;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Storages.Providers.EntityFramework;
using Storages.Providers.EntityFramework.Implementations;

namespace Storages.Tests;

public class ProductStorageTest : IDisposable
{
    public ProductStorageTest()
    {
        _dbContext = new Service2Context(new DbContextOptionsBuilder<Service2Context>().UseSqlite("Data Source=./TestDb.db")
            .Options);
        _productStorage = new ProductStorage(_dbContext);
        _manufacturerStorage = new ManufacturerStorage(_dbContext);
        _warehouseStorage = new WarehouseStorage(_dbContext);
        _faker = new Faker("ru");
    }
    
    [Fact]
    public async Task Successful_Insert()
    {
        const int countOfTestManufacturers = 8;
        var testManufacturers = new Manufacturer[countOfTestManufacturers];
        for (var i = 0; i < countOfTestManufacturers; i++)
        {
            testManufacturers[i] = new Manufacturer(_faker.Random.Guid().ToString(), _faker.Address.FullAddress(),
                _faker.Company.CompanyName());
            await _manufacturerStorage.Insert(testManufacturers[i]);
        }
        
        const int countOfTestWarehouses = 6;
        var testWarehouses = new Warehouse[countOfTestWarehouses];
        for (var i = 0; i < countOfTestWarehouses; i++)
        {
            testWarehouses[i] = new Warehouse(_faker.Random.Guid().ToString(), _faker.Address.FullAddress(),
                (_faker.Address.Longitude(), _faker.Address.Latitude()));
            await _warehouseStorage.Insert(testWarehouses[i]);
        }
        
        const int countOfTestProducts = 20;
        var testProducts = new Product[countOfTestProducts];
        for (var i = 0; i < countOfTestProducts; i++)
        {
            testProducts[i] = new Product(_faker.Random.Guid().ToString(), _faker.Commerce.ProductName(), new Lazy<Manufacturer>(() => _faker.Random.ArrayElement(testManufacturers)), _faker.Date.Between(DateTime.Now.AddDays(-7), DateTime.Now.AddDays(7)), new Lazy<Warehouse>(() => _faker.Random.ArrayElement(testWarehouses)));
            await _productStorage.Insert(testProducts[i]);
        }
        var productsFromStorage = (await _productStorage.GetAll()).ToList();
        
        for (var i = 0; i < countOfTestProducts; i++)
        {
            Assert.Contains(productsFromStorage, p => p.Equals(testProducts[i]));
            Assert.Contains(testManufacturers, m => m.Equals(testProducts[i].Manufacturer.Value) && m.Equals(productsFromStorage[i].Manufacturer.Value));
            Assert.Contains(testWarehouses, w => w.Equals(testProducts[i].Warehouse.Value) && w.Equals(productsFromStorage[i].Warehouse.Value));
        }
    }
    
    public void Dispose()
    {
        _dbContext.Dispose();
    }

    private readonly Service2Context _dbContext;

    private readonly ProductStorage _productStorage;
    
    private readonly ManufacturerStorage _manufacturerStorage;
    
    private readonly WarehouseStorage _warehouseStorage;

    private readonly Faker _faker;
}