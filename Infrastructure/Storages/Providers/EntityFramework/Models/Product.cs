using System.ComponentModel.DataAnnotations.Schema;

namespace Storages.Providers.EntityFramework.Models;

public class Product : BaseModel
{
    public string Name { get; set; } = null!;
    
    [ForeignKey(nameof(Manufacturer))]
    public string ManufacturerGuid { get; set; } = null!;

    public virtual Manufacturer? Manufacturer { get; set; }

    public DateTime ReceiptDate { get; set; }
    
    [ForeignKey(nameof(Warehouse))]
    public string WarehouseGuid { get; set; } = null!;

    public virtual Warehouse? Warehouse { get; set; }
    
    [ForeignKey(nameof(Supply))]
    public string? SupplyGuid { get; set; }

    public virtual Supply? Supply { get; set; }
}