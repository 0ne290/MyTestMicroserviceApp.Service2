using FluentResults;

namespace Domain.Models;

public class Article(Lazy<IEnumerable<Product>> products) : BaseModel
{
    public required string Name { get; set; }
    
    public required Lazy<Manufacturer> Manufacturer { get; set; }
    
    public IReadOnlyCollection<Product> Products => _products.Value;
    
    private readonly Lazy<HashSet<Product>> _products = new(() => new HashSet<Product>(products.Value));
}