using Domain.Entities;
using Domain.Interfaces;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Storages.Providers.EntityFramework.Mappers;

namespace Storages.Providers.EntityFramework.Implementations;

public class SupplyStorage : ISupplyStorage
{
    public SupplyStorage(Service2Context dbContext, IProductStorage productStorage)
    {
        _dbContext = dbContext;
        _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTrackingWithIdentityResolution;
        _productStorage = productStorage;
    }
    
    public async Task<IEnumerable<Supply>> GetAll() => (await _dbContext.Supplies.ToListAsync()).Select(s =>
        SupplyMapper.ModelToEntity(s, async () => await _productStorage.GetAllBySupplyGuid(s.Guid)));
    
    public async Task<Result> Insert(Supply supply)
    {
        await _dbContext.Supplies.AddAsync(SupplyMapper.EntityToModel(supply));
        foreach (var product in await supply.GetProducts())
            _dbContext.Update(await ProductMapper.EntityToModel(product, supply.Guid));
        await _dbContext.SaveChangesAsync();
        _dbContext.ChangeTracker.Clear();
        
        return Result.Ok();
    }

    public async Task<Result> Update(Supply supply)
    {
        _dbContext.Update(SupplyMapper.EntityToModel(supply));
        await _dbContext.SaveChangesAsync();
        _dbContext.ChangeTracker.Clear();
        
        return Result.Ok();
    }
    
    private readonly Service2Context _dbContext;

    private readonly IProductStorage _productStorage;
}