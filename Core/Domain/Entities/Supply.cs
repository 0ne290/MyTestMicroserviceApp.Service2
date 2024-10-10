using FluentResults;

namespace Domain.Entities;

public class Supply(string guid, DateTime date, Lazy<Task<Warehouse>> warehouse, string externalStoreGuid,
    Lazy<Task<IEnumerable<Product>>> products) : BaseEntity(guid)
{
    public async Task<IReadOnlyCollection<Product>> GetProducts() => await _products.Value;

    public async Task<Result> AddProduct(Product product) => (await _products.Value).Add(product)
        ? Result.Ok()
        : Result.Fail($"The product {product.Guid} is already included in the supply.");
    
    public DateTime Date { get; } = date;

    public Lazy<Task<Warehouse>> Warehouse { get; } = warehouse;

    public string ExternalStoreGuid { get; } = externalStoreGuid;

    private readonly Lazy<Task<HashSet<Product>>> _products = new(async () => new HashSet<Product>(await products.Value));
}