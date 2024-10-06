using Domain.Entities;
using Domain.Interfaces;
using FluentResults;
using Storages.Providers.EntityFramework.Mappers;

namespace Storages.Providers.EntityFramework.Implementations;

public class ProductStorage(Service2Context dbContext) : IProductStorage
{
    public async Task<ICollection<Product>> GetAll() =>
        await Task.FromResult(_dbContext.Products.AsEnumerable().Select(product => ProductMapper.ModelToEntity(product,
            new Lazy<Manufacturer>(() =>
            {
                _dbContext.Entry(product)
                    .Reference(p => p.Manufacturer)
                    .Load();
                return ManufacturerMapper.ModelToEntity(product.Manufacturer!);
            }), new Lazy<Warehouse>(() =>
            {
                _dbContext.Entry(product)
                    .Reference(p => p.Warehouse)
                    .Load();
                return WarehouseMapper.ModelToEntity(product.Warehouse!);
            }))).ToList());
    
    public async Task<Result> Insert(Product product)
    {
        await _dbContext.Products.AddAsync(ProductMapper.EntityToModel(product));
        await _dbContext.SaveChangesAsync();
        
        return Result.Ok();
    }

    public async Task<Result> Update(Product product)
    {
        _dbContext.Update(ProductMapper.EntityToModel(product));
        await _dbContext.SaveChangesAsync();
        
        return Result.Ok();
    }
    
    private readonly Service2Context _dbContext = dbContext;
}