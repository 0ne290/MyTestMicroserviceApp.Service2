using Entities = Domain.Entities;

namespace Storages.Providers.EntityFramework.Mappers;

public static class SupplyMapper
{
    public static async Task<Models.Supply> EntityToModel(Entities.Supply entity)
    {
        var productEntities = await entity.GetProducts();
        var productModels = new List<Models.Product>(productEntities.Count);
        foreach (var productEntity in productEntities)
            productModels.Add(await ProductMapper.EntityToModel(productEntity));

        return new Models.Supply
        {
            Guid = entity.Guid, Date = entity.Date, ExternalStoreGuid = entity.ExternalStoreGuid,
            Products = productModels
        };
    }

    public static Entities.Supply
        ModelToEntity(Models.Supply model, Func<Task<IEnumerable<Entities.Product>>> products) => new(model.Guid,
        model.Date, model.ExternalStoreGuid, new Lazy<Task<IEnumerable<Entities.Product>>>(products));
}