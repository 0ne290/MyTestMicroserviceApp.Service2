using Entities = Domain.Entities;

namespace Storages.Providers.EntityFramework.Mappers;

public static class ProductMapper
{
    public static async Task<Models.Product> EntityToModel(Entities.Product entity) => new()
    {
        Guid = entity.Guid, Name = entity.Name, ManufacturerGuid = (await entity.Manufacturer.Value).Guid,
        ReceiptDate = entity.ReceiptDate, WarehouseGuid = (await entity.Warehouse.Value).Guid
    };
    
    public static Entities.Product ModelToEntity(Models.Product model, Lazy<Task<Entities.Manufacturer>> manufacturer, Lazy<Task<Entities.Warehouse>> warehouse) =>
        new(model.Guid, model.Name, manufacturer, model.ReceiptDate, warehouse);
}