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

    public async Task<IEnumerable<Product>> GetAll() => (await _dbContext.Products.ToListAsync()).Select(p =>
        ProductMapper.ModelToEntity(p, async () => await GetManufacturerByGuid(p.ManufacturerGuid),
            async () => await GetWarehouseByGuid(p.WarehouseGuid), p.SupplyGuid == null ? null : async () => await GetSupplyByGuid(p.SupplyGuid)));

    public async Task<IEnumerable<Product>> GetAllByGuids(IEnumerable<string> guids) =>
        (await _dbContext.Products.Where(p => guids.Contains(p.Guid)).ToListAsync()).Select(p =>
            ProductMapper.ModelToEntity(p, async () => await GetManufacturerByGuid(p.ManufacturerGuid),
                async () => await GetWarehouseByGuid(p.WarehouseGuid), p.SupplyGuid == null ? null : async () => await GetSupplyByGuid(p.SupplyGuid)));

    private async Task<Manufacturer> GetManufacturerByGuid(string guid) =>
        ManufacturerMapper.ModelToEntity(await _dbContext.Manufacturers.FirstAsync(m => m.Guid == guid));
    
    private async Task<Warehouse> GetWarehouseByGuid(string guid) =>
        WarehouseMapper.ModelToEntity(await _dbContext.Warehouses.FirstAsync(w => w.Guid == guid));
    
    private async Task<Supply> GetSupplyByGuid(string guid)
    {
        var supply = await _dbContext.Supplies.FirstAsync(s => s.Guid == guid);

        return SupplyMapper.ModelToEntity(supply, async () => await GetAllBySupplyGuid(supply.Guid));
    }
    
    private async Task<IEnumerable<Product>> GetAllBySupplyGuid(string supplyGuid) => (await _dbContext.Products.Where(p => p.SupplyGuid == supplyGuid).ToListAsync()).Select(p =>
        ProductMapper.ModelToEntity(p, async () => await GetManufacturerByGuid(p.ManufacturerGuid),
            async () => await GetWarehouseByGuid(p.WarehouseGuid), p.SupplyGuid == null ? null : async () => await GetSupplyByGuid(p.SupplyGuid)));
    
    public async Task<Result> Insert(Product product)
    {
        await _dbContext.Products.AddAsync(await ProductMapper.EntityToModel(product));
        await _dbContext.SaveChangesAsync();
        _dbContext.ChangeTracker.Clear();
        
        return Result.Ok();
    }

    private readonly Service2Context _dbContext;
}