using Domain.Entities;
using Domain.Interfaces;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Storages.Providers.EntityFramework.Mappers;

namespace Storages.Providers.EntityFramework.Implementations;

public class ProductStorage : IProductStorage
{
    public ProductStorage(Service2Context dbContext, IManufacturerStorage manufacturerStorage, IWarehouseStorage warehouseStorage, ISupplyStorage supplyStorage)
    {
        _dbContext = dbContext;
        _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTrackingWithIdentityResolution;
        _manufacturerStorage = manufacturerStorage;
        _warehouseStorage = warehouseStorage;
        _supplyStorage = supplyStorage;
    }

    public async Task<IEnumerable<Product>> GetAll() => (await _dbContext.Products.ToListAsync()).Select(p =>
        ProductMapper.ModelToEntity(p, async () => await _manufacturerStorage.GetByGuid(p.ManufacturerGuid),
            async () => await _warehouseStorage.GetByGuid(p.WarehouseGuid), p.SupplyGuid == null ? null : async () => await _supplyStorage.GetByGuid(p.SupplyGuid)));
    
    public async Task<IEnumerable<Product>> GetAllBySupplyGuid(string supplyGuid) => (await _dbContext.Products.Where(p => p.SupplyGuid == supplyGuid).ToListAsync()).Select(p =>
        ProductMapper.ModelToEntity(p, async () => await _manufacturerStorage.GetByGuid(p.ManufacturerGuid),
            async () => await _warehouseStorage.GetByGuid(p.WarehouseGuid), p.SupplyGuid == null ? null : async () => await _supplyStorage.GetByGuid(p.SupplyGuid)));

    public async Task<IEnumerable<Product>> GetAllByGuids(IEnumerable<string> guids) =>
        (await _dbContext.Products.Where(p => guids.Contains(p.Guid)).ToListAsync()).Select(p =>
            ProductMapper.ModelToEntity(p, async () => await _manufacturerStorage.GetByGuid(p.ManufacturerGuid),
                async () => await _warehouseStorage.GetByGuid(p.WarehouseGuid), p.SupplyGuid == null ? null : async () => await _supplyStorage.GetByGuid(p.SupplyGuid)));

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
    
    private readonly ISupplyStorage _supplyStorage;
}