using Application.Dtos;
using Domain.Entities;

namespace Application.Mappers;

public static class ProductMapper
{
    public static async Task<ProductDto> EntityToDto(Product entity) => new()
    {
        Guid = entity.Guid, Name = entity.Name, ManufacturerGuid = (await entity.Manufacturer.Value).Guid,
        ReceiptDate = entity.ReceiptDate, WarehouseGuid = (await entity.Warehouse.Value).Guid,
        SupplyGuid = entity.Supply == null ? null : (await entity.Supply.Value).Guid
    };
}