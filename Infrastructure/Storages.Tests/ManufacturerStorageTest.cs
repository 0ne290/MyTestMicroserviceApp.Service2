using Bogus;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Storages.Providers.EntityFramework;
using Storages.Providers.EntityFramework.Implementations;

namespace Storages.Tests;

public class ManufacturerStorageTest : IDisposable
{
    public ManufacturerStorageTest()
    {
        _dbContext = new Service2Context(new DbContextOptionsBuilder<Service2Context>().UseSqlite("Data Source=./TestDb.db")
            .Options);
        _manufacturerStorage = new ManufacturerStorage(_dbContext);
        _faker = new Faker("ru");
    }
    
    [Fact]
    public async Task Successful_Insert()
    {
        const int countOfTestManufacturers = 20;
        var testManufacturers = new Manufacturer[countOfTestManufacturers];
        for (var i = 0; i < countOfTestManufacturers; i++)
        {
            testManufacturers[i] = new Manufacturer(_faker.Random.Guid().ToString(), _faker.Address.FullAddress(),
                _faker.Company.CompanyName());
            await _manufacturerStorage.Insert(testManufacturers[i]);
        }
        var manufacturersFromStorage = (await _manufacturerStorage.GetAll()).ToList();
        
        for (var i = 0; i < countOfTestManufacturers; i++)
            Assert.Contains(manufacturersFromStorage, m => m.Equals(testManufacturers[i]));
    }
    
    public void Dispose()
    {
        _dbContext.Dispose();
    }

    private readonly Service2Context _dbContext;

    private readonly ManufacturerStorage _manufacturerStorage;

    private readonly Faker _faker;
}