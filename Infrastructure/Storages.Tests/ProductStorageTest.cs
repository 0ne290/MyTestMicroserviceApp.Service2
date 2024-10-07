using Bogus;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Storages.Providers.EntityFramework;
using Storages.Providers.EntityFramework.Implementations;

namespace Storages.Tests;

// TODO: Результаты тестирования: глобально отключить отслеживание в хранилищах; если даже с отключенным отслеживанием сущности все равно отслеживаются после методов Update и Insert, добавить в эти методы вызов метода сброса отслеживаемых сущностей; рассмотреть вариант замены методов контекста Update и AddAsync на не отслеживающие аналоги (начни копать с ExecuteUpdate и ExecuteDelete); протестировать хранилище поставок; провести рефакторинг в связи с добавлением в проект асинхронной ленивой загрузки (особенно много рефакторинга требуется в слое хранилищ)
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
        var testManufacturers = new List<Manufacturer>(countOfTestManufacturers);
        for (var i = 0; i < countOfTestManufacturers; i++)
        {
            testManufacturers.Add(new Manufacturer(_faker.Random.Guid().ToString(), _faker.Address.FullAddress(),
                _faker.Company.CompanyName()));
            await _manufacturerStorage.Insert(testManufacturers[i]);
        }
        testManufacturers = (await _manufacturerStorage.GetAll()).ToList();
        
        
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
            testProducts[i] = new Product(_faker.Random.Guid().ToString(), _faker.Commerce.ProductName(), new Lazy<Task<Manufacturer>>(() => Task.FromResult(_faker.Random.ListItem(testManufacturers))), _faker.Date.Between(DateTime.Now.AddDays(-7), DateTime.Now.AddDays(7)), new Lazy<Task<Warehouse>>(() => Task.FromResult(_faker.Random.ArrayElement(testWarehouses))));
            await _productStorage.Insert(testProducts[i]);
        }
        var productsFromStorage = (await _productStorage.GetAll()).ToList();
        
        for (var i = 0; i < countOfTestProducts; i++)
        {
            var me = await testProducts[i].Manufacturer.Value;
            var ma = await productsFromStorage[i].Manufacturer.Value;
            var we = await testProducts[i].Warehouse.Value;
            var wa = await productsFromStorage[i].Warehouse.Value;
            Assert.Contains(productsFromStorage, p => p.Equals(testProducts[i]));
            Assert.Contains(testManufacturers, m => m.Equals(me) && m.Equals(ma));
            Assert.Contains(testWarehouses, w => w.Equals(we) && w.Equals(wa));
        }
    }

    [Fact]
    public async Task Successful_Update()
    {
        var product = (await _productStorage.GetAll()).First();
        var oldManufacturer = await product.Manufacturer.Value;
        var newManufacturer = new Manufacturer(_faker.Random.Guid().ToString(), _faker.Address.FullAddress(),
                    _faker.Company.CompanyName());
        await _manufacturerStorage.Insert(newManufacturer);
        
        product.Name = "NewName";
        await _productStorage.Update(product);

        product.Manufacturer = new Lazy<Task<Manufacturer>>(Task.FromResult(newManufacturer));
        await _productStorage.Update(product);
        
        product = (await _productStorage.GetAll()).First(p => p.Guid == product.Guid);
        oldManufacturer = (await _manufacturerStorage.GetAll()).First(m => m.Guid == oldManufacturer.Guid);
        newManufacturer = (await _manufacturerStorage.GetAll()).First(m => m.Guid == newManufacturer.Guid);
        
        Assert.Equal(newManufacturer, await product.Manufacturer.Value);
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