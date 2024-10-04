namespace Storages.Providers.EntityFramework;

public class Article
{
    public required string Name { get; init; }
    
    public required string ManufacturerGuid { get; init; }
    
    public virtual required Manufacturer Manufacturer { get; init; }
}