using Domain.Entities;
using Domain.Interfaces;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Storages.Providers.EntityFramework.Mappers;

namespace Storages.Providers.EntityFramework.Implementations;

public class ProductStorage(Service2Context dbContext) : IProductStorage
{
    public async Task<IEnumerable<Product>> GetAll() =>
        await Task.FromResult(_dbContext.Products.AsNoTracking().AsEnumerable().Select(product => ProductMapper.ModelToEntity(product,
            new Lazy<Task<Manufacturer>>(async () => ManufacturerMapper.ModelToEntity(await _dbContext.Manufacturers.AsNoTracking().SingleAsync(m => m.Guid == product.ManufacturerGuid))), new Lazy<Task<Warehouse>>(async () => WarehouseMapper.ModelToEntity(await _dbContext.Warehouses.AsNoTracking().SingleAsync(w => w.Guid == product.WarehouseGuid))))));
    
    public async Task<Result> Insert(Product product)
    {
        await _dbContext.Products.AddAsync(await ProductMapper.EntityToModel(product));
        await _dbContext.SaveChangesAsync();
        
        return Result.Ok();
    }

    public async Task<Result> Update(Product product)
    {
        _dbContext.Update(await ProductMapper.EntityToModel(product));
        await _dbContext.SaveChangesAsync();
        
        return Result.Ok();
    }
    
    private readonly Service2Context _dbContext = dbContext;
}