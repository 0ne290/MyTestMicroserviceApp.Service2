using Domain.Entities;
using Domain.Interfaces;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Storages.Providers.EntityFramework.Mappers;

namespace Storages.Providers.EntityFramework.Implementations;

public class ProductStorage : IProductStorage
{
    public ProductStorage(Service2Context dbContext)
    {
        _dbContext = dbContext;
        _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTrackingWithIdentityResolution;
    }

    // Наглядный пример кейса, в котором правильнее использовать внедрение зависимостей через метод. Ну или
    // централизованное ленивое получение зависимостей по требованию, но это уже анти-паттерн "Сервис Локатор"
    public async Task<IEnumerable<Product>> GetAll() => (await _dbContext.Products.ToListAsync()).Select(p =>
        ProductMapper.ModelToEntity(p, async () => await GetManufacturerByGuid(p.ManufacturerGuid),
            async () => await GetWarehouseByGuid(p.WarehouseGuid)));
    
    public async Task<Product> GetByGuid(string guid)
    {
        var product = await _dbContext.Products.SingleAsync(p => p.Guid == guid);
        return ProductMapper.ModelToEntity(product, async () => await GetManufacturerByGuid(product.ManufacturerGuid), async () => await GetWarehouseByGuid(product.WarehouseGuid));
    }
    
    private async Task<Manufacturer> GetManufacturerByGuid(string guid) =>
        ManufacturerMapper.ModelToEntity(await _dbContext.Manufacturers.SingleAsync(m => m.Guid == guid));
    
    private async Task<Warehouse> GetWarehouseByGuid(string guid) => WarehouseMapper.ModelToEntity(await _dbContext.Warehouses.SingleAsync(w => w.Guid == guid));
    
    public async Task<Result> Insert(Product product)
    {
        await _dbContext.Products.AddAsync(await ProductMapper.EntityToModel(product));
        await _dbContext.SaveChangesAsync();
        _dbContext.ChangeTracker.Clear();
        
        return Result.Ok();
    }

    public async Task<Result> Update(Product product)
    {
        _dbContext.Update(await ProductMapper.EntityToModel(product));
        await _dbContext.SaveChangesAsync();
        _dbContext.ChangeTracker.Clear();
        
        return Result.Ok();
    }

    private readonly Service2Context _dbContext;
}