namespace Domain.Entities;

public class Supply(string guid, DateTime date, string externalStoreGuid,
    Lazy<Task<IEnumerable<Product>>> products) : BaseEntity(guid)
{
    public async Task<IReadOnlyCollection<Product>> GetProducts() => await _products.Value;
    
    public DateTime Date { get; } = date;

    public string ExternalStoreGuid { get; } = externalStoreGuid;

    private readonly Lazy<Task<List<Product>>> _products = new(async () => (await products.Value).ToList());
}