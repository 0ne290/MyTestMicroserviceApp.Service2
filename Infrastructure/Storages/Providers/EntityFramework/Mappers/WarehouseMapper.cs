using Entities = Domain.Entities;

namespace Storages.Providers.EntityFramework.Mappers;

public static class WarehouseMapper
{
    public static Models.Warehouse EntityToModel(Entities.Warehouse entity) => new()
    {
        Guid = entity.Guid, Address = entity.Address, GeolocationLongitude = entity.Geolocation.Longitude,
        GeolocationLatitude = entity.Geolocation.Latitude
    };
}