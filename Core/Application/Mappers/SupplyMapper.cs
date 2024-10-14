using Application.Dtos;
using Domain.Entities;

namespace Application.Mappers;

public static class SupplyMapper
{
    public static async Task<SupplyDto> EntityToDto(Supply entity) => new()
    {
        Guid = entity.Guid, ProductGuids = (await entity.GetProducts()).Select(p => p.Guid).ToList(),
        Date = entity.Date, ExternalStoreGuid = entity.ExternalStoreGuid
    };
}