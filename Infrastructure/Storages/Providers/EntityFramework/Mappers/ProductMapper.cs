using Entities = Domain.Entities;

namespace Storages.Providers.EntityFramework.Mappers;

public static class ProductMapper
{
    public static Models.Product EntityToModel(Entities.Product entity) => new()
    {
        Guid = entity.Guid, Name = entity.Name, ManufacturerGuid = entity.Manufacturer.Value.Guid,
        ReceiptDate = entity.ReceiptDate, WarehouseGuid = entity.Warehouse.Value.Guid
    };
}