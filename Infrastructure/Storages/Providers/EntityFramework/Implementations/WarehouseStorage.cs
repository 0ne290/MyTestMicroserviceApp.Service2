using Domain.Entities;
using Domain.Interfaces;
using FluentResults;
using Storages.Providers.EntityFramework.Mappers;

namespace Storages.Providers.EntityFramework.Implementations;

public class WarehouseStorage(Service2Context dbContext) : IWarehouseStorage
{ 
    public async Task<Result> Insert(Warehouse warehouse)
    {
        await _dbContext.Warehouses.AddAsync(WarehouseMapper.EntityToModel(warehouse));
        await _dbContext.SaveChangesAsync();
        
        return Result.Ok();
    }

    public async Task<Result> Update(Warehouse warehouse)
    {
        _dbContext.Update(WarehouseMapper.EntityToModel(warehouse));
        await _dbContext.SaveChangesAsync();
        
        return Result.Ok();
    }
    
    private readonly Service2Context _dbContext = dbContext;
}