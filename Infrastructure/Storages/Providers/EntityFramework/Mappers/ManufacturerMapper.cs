using Entities = Domain.Entities;

namespace Storages.Providers.EntityFramework.Mappers;

public static class ManufacturerMapper
{
    public static Models.Manufacturer EntityToModel(Entities.Manufacturer entity) => new()
        { Guid = entity.Guid, Address = entity.Address, Name = entity.Name };

    public static Entities.Manufacturer ModelToEntity(Models.Manufacturer model) =>
        new(model.Guid, model.Address, model.Name);
}