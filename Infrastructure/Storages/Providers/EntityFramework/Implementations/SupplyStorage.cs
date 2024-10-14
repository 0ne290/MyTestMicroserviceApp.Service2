using Domain.Entities;
using Domain.Interfaces;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Storages.Providers.EntityFramework.Mappers;

namespace Storages.Providers.EntityFramework.Implementations;

public class SupplyStorage : ISupplyStorage
{
    public SupplyStorage(Service2Context dbContext)
    {
        _dbContext = dbContext;
        _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTrackingWithIdentityResolution;
    }
    
    public async Task<IEnumerable<Supply>> GetAll() => (await _dbContext.Supplies.ToListAsync()).Select(s => SupplyMapper.ModelToEntity(s, async () => await GetAllProductsBySupplyGuid(s.Guid)));

    public async Task<Supply> GetByGuid(string guid)
    {
        var supply = await _dbContext.Supplies.FirstAsync(s => s.Guid == guid);

        return SupplyMapper.ModelToEntity(supply, async () => await GetAllProductsBySupplyGuid(supply.Guid));
    }
    
    private async Task<IEnumerable<Product>> GetAllProductsBySupplyGuid(string supplyGuid) => (await _dbContext.Products.Where(p => p.SupplyGuid == supplyGuid).ToListAsync()).Select(p =>
        ProductMapper.ModelToEntity(p, async () => await GetManufacturerByGuid(p.ManufacturerGuid),
            async () => await GetWarehouseByGuid(p.WarehouseGuid), p.SupplyGuid == null ? null : async () => await GetByGuid(p.SupplyGuid)));
    
    private async Task<Manufacturer> GetManufacturerByGuid(string guid) =>
        ManufacturerMapper.ModelToEntity(await _dbContext.Manufacturers.FirstAsync(m => m.Guid == guid));
    
    private async Task<Warehouse> GetWarehouseByGuid(string guid) =>
        WarehouseMapper.ModelToEntity(await _dbContext.Warehouses.FirstAsync(w => w.Guid == guid));

    public async Task<Result> Insert(Supply supply)
    {
        await _dbContext.Supplies.AddAsync(SupplyMapper.EntityToModel(supply));
        await _dbContext.SaveChangesAsync();
        
        var productGuids = (await supply.GetProducts()).Select(p => p.Guid);
        await _dbContext.Products.Where(p => productGuids.Contains(p.Guid))
            .ExecuteUpdateAsync(setters => setters.SetProperty(p => p.SupplyGuid, supply.Guid));
        
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
}