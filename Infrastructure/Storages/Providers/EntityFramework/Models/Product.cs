namespace Storages.Providers.EntityFramework.Models;

public class Product : BaseModel
{
    public string Name { get; set; } = null!;

    public string ManufacturerGuid { get; set; } = null!;

    public DateTime ReceiptDate { get; set; }
    
    public string WarehouseGuid { get; set; } = null!;
    
    public string? SupplyGuid { get; set; }
}
