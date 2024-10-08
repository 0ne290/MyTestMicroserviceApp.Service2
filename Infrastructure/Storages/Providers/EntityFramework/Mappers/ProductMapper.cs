using Entities = Domain.Entities;

namespace Storages.Providers.EntityFramework.Mappers;

public static class ProductMapper
{
    public static async Task<Models.Product> EntityToModel(Entities.Product entity, string? supplyGuid = null) => new()
    {
        Guid = entity.Guid, Name = entity.Name, ManufacturerGuid = (await entity.Manufacturer.Value).Guid,
        ReceiptDate = entity.ReceiptDate, WarehouseGuid = (await entity.Warehouse.Value).Guid, SupplyGuid = supplyGuid
    };

    public static Entities.Product ModelToEntity(Models.Product model, Func<Task<Entities.Manufacturer>> manufacturer,
        Func<Task<Entities.Warehouse>> warehouse) => new(model.Guid, model.Name,
        new Lazy<Task<Entities.Manufacturer>>(manufacturer), model.ReceiptDate,
        new Lazy<Task<Entities.Warehouse>>(warehouse));
}