using Domain.Entities;
using Domain.Interfaces;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Storages.Providers.EntityFramework.Mappers;

namespace Storages.Providers.EntityFramework.Implementations;

public class ManufacturerStorage : IManufacturerStorage
{
    public ManufacturerStorage(Service2Context dbContext)
    {
        _dbContext = dbContext;
        _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTrackingWithIdentityResolution;
    }
    
    public async Task<IEnumerable<Manufacturer>> GetAll() =>
        await Task.FromResult(_dbContext.Manufacturers.AsEnumerable().Select(ManufacturerMapper.ModelToEntity));

    public async Task<Manufacturer> GetByGuid(string guid) =>
        ManufacturerMapper.ModelToEntity(await _dbContext.Manufacturers.SingleAsync(m => m.Guid == guid));
    
    public async Task<Result> Insert(Manufacturer manufacturer)
    {
        await _dbContext.Manufacturers.AddAsync(ManufacturerMapper.EntityToModel(manufacturer));
        await _dbContext.SaveChangesAsync();
        _dbContext.ChangeTracker.Clear();
        
        return Result.Ok();
    }

    public async Task<Result> Update(Manufacturer manufacturer)
    {
        _dbContext.Update(ManufacturerMapper.EntityToModel(manufacturer));
        await _dbContext.SaveChangesAsync();
        _dbContext.ChangeTracker.Clear();
        
        return Result.Ok();
    }
    
    private readonly Service2Context _dbContext;
}