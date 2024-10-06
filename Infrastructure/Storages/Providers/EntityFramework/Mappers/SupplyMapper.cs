using Entities = Domain.Entities;

namespace Storages.Providers.EntityFramework.Mappers;

public static class SupplyMapper
{
    public static Models.Supply EntityToModel(Entities.Supply entity) => new()
    {
        Guid = entity.Guid, Date = entity.Date, WarehouseGuid = entity.Warehouse.Value.Guid,
        ExternalStoreGuid = entity.ExternalStoreGuid,
        Products = entity.Products.Select(ProductMapper.EntityToModel).ToList()
    };
}