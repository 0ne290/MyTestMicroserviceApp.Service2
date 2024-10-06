using Domain.Entities;
using Domain.Interfaces;
using FluentResults;
using Storages.Providers.EntityFramework.Mappers;

namespace Storages.Providers.EntityFramework.Implementations;

public class ProductStorage(Service2Context dbContext) : IProductStorage
{ 
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