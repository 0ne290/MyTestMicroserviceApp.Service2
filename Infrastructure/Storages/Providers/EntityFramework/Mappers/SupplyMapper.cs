using Entities = Domain.Entities;

namespace Storages.Providers.EntityFramework.Mappers;

public static class SupplyMapper
{
    public static Models.Supply EntityToModel(Entities.Supply entity) => new()
    {
        Guid = entity.Guid, Date = entity.Date, ExternalStoreGuid = entity.ExternalStoreGuid
    };

    public static Entities.Supply
        ModelToEntity(Models.Supply model, Func<Task<IEnumerable<Entities.Product>>> products) => new(model.Guid,
        model.Date, model.ExternalStoreGuid, new Lazy<Task<IEnumerable<Entities.Product>>>(products));
}