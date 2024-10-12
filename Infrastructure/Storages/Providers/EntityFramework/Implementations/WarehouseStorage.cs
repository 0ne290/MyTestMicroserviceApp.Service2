using Domain.Entities;
using Domain.Interfaces;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Storages.Providers.EntityFramework.Mappers;

namespace Storages.Providers.EntityFramework.Implementations;

public class WarehouseStorage : IWarehouseStorage
{
    public WarehouseStorage(Service2Context dbContext)
    {
        _dbContext = dbContext;
        _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTrackingWithIdentityResolution;
    }

    public async Task<IEnumerable<Warehouse>> GetAll() =>
        (await _dbContext.Warehouses.ToListAsync()).Select(WarehouseMapper.ModelToEntity);

    public async Task<Warehouse> GetByGuid(string guid) =>
        WarehouseMapper.ModelToEntity(await _dbContext.Warehouses.SingleAsync(w => w.Guid == guid));
    
    public async Task<bool> ExistsByGuid(string guid) => await _dbContext.Warehouses.AnyAsync(w => w.Guid == guid);

    public async Task<bool> ExistsByGeolocation((double Longitude, double Latitude) geolocation) =>
        await _dbContext.Warehouses.AnyAsync(w =>
            Math.Abs(w.GeolocationLongitude - geolocation.Longitude) < 0.000001 &&
            Math.Abs(w.GeolocationLatitude - geolocation.Latitude) < 0.000001);

    public async Task<bool> ExistsByAddress(string address) => await _dbContext.Warehouses.AnyAsync(w => w.Address == address);

    public async Task<Result> Insert(Warehouse warehouse)
    {
        await _dbContext.Warehouses.AddAsync(WarehouseMapper.EntityToModel(warehouse));
        await _dbContext.SaveChangesAsync();
        _dbContext.ChangeTracker.Clear();
        
        return Result.Ok();
    }

    public async Task<Result> Update(Warehouse warehouse)
    {
        _dbContext.Update(WarehouseMapper.EntityToModel(warehouse));
        await _dbContext.SaveChangesAsync();
        _dbContext.ChangeTracker.Clear();
        
        return Result.Ok();
    }

    private readonly Service2Context _dbContext;
}