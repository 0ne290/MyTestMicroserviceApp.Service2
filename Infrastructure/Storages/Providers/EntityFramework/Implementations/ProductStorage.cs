using Domain.Entities;
using Domain.Interfaces;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Storages.Providers.EntityFramework.Mappers;

namespace Storages.Providers.EntityFramework.Implementations;

public class ProductStorage : IProductStorage
{
    public ProductStorage(Service2Context dbContext, IManufacturerStorage manufacturerStorage, IWarehouseStorage warehouseStorage)
    {
        _dbContext = dbContext;
        _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTrackingWithIdentityResolution;
        _manufacturerStorage = manufacturerStorage;
        _warehouseStorage = warehouseStorage;
    }

    // Наглядный пример кейса, в котором правильнее использовать внедрение зависимостей через метод. Ну или
    // централизованное ленивое получение зависимостей по требованию, но это уже анти-паттерн "Сервис Локатор"
    public async Task<IEnumerable<Product>> GetAll() =>
        await Task.FromResult(_dbContext.Products.AsEnumerable().Select(product =>
            ProductMapper.ModelToEntity(product, _manufacturerStorage, _warehouseStorage)));
    
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
    
    private readonly IManufacturerStorage _manufacturerStorage;
    
    private readonly IWarehouseStorage _warehouseStorage;
}