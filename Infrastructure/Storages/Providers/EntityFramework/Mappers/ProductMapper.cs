using Domain.Interfaces;
using Entities = Domain.Entities;

namespace Storages.Providers.EntityFramework.Mappers;

public static class ProductMapper
{
    public static async Task<Models.Product> EntityToModel(Entities.Product entity) => new()
    {
        Guid = entity.Guid, Name = entity.Name, ManufacturerGuid = (await entity.Manufacturer.Value).Guid,
        ReceiptDate = entity.ReceiptDate, WarehouseGuid = (await entity.Warehouse.Value).Guid
    };

    public static Entities.Product
        ModelToEntity(Models.Product model, IManufacturerStorage manufacturerStorage,
            IWarehouseStorage warehouseStorage) => new(model.Guid, model.Name,
        new Lazy<Task<Entities.Manufacturer>>(async () => await manufacturerStorage.GetByGuid(model.ManufacturerGuid)),
        model.ReceiptDate,
        new Lazy<Task<Entities.Warehouse>>(async () => await warehouseStorage.GetByGuid(model.WarehouseGuid)));
}