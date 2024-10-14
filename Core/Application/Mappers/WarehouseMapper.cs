using Application.Dtos;
using Domain.Entities;

namespace Application.Mappers;

public static class WarehouseMapper
{
    public static WarehouseDto EntityToDto(Warehouse entity) => new()
    {
        Guid = entity.Guid, Address = entity.Address, GeolocationLongitude = entity.Geolocation.Longitude,
        GeolocationLatitude = entity.Geolocation.Latitude
    };
}