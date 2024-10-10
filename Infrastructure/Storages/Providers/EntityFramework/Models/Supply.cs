namespace Storages.Providers.EntityFramework.Models;

public class Supply : BaseModel
{
    public DateTime Date { get; set; }
    
    public string WarehouseGuid { get; set; } = null!;

    public string ExternalStoreGuid { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}