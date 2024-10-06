namespace Domain.Entities;

public class Supply(string guid, DateTime date, Lazy<Warehouse> warehouse, string externalStoreGuid,
    Lazy<IEnumerable<Product>> products) : BaseEntity(guid)
{
    public bool AddProduct(Product product) => _products.Value.Add(product);
    
    public bool RemoveProduct(Product product) => _products.Value.Remove(product);
    
    public DateTime Date { get; } = date;

    public Lazy<Warehouse> Warehouse { get; } = warehouse;

    public string ExternalStoreGuid { get; } = externalStoreGuid;

    public IReadOnlyCollection<Product> Products => _products.Value;

    private readonly Lazy<HashSet<Product>> _products = new(() => new HashSet<Product>(products.Value));
}