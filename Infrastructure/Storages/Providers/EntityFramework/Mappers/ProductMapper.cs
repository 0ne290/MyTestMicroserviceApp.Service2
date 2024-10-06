using Entities = Domain.Entities;

namespace Storages.Providers.EntityFramework.Mappers;

public static class ProductMapper
{
    public static Models.Product EntityToModel(Entities.Product entity) => new()
    {
        Guid = entity.Guid, Name = entity.Name, ManufacturerGuid = entity.Manufacturer.Value.Guid,
        ReceiptDate = entity.ReceiptDate, WarehouseGuid = entity.Warehouse.Value.Guid
    };
    
    public static Entities.Product ModelToEntity(Models.Product model, Lazy<Entities.Manufacturer> manufacturer, Lazy<Entities.Warehouse> warehouse) =>
        new(model.Guid, model.Name, manufacturer, model.ReceiptDate, warehouse);
}