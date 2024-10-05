using System.ComponentModel.DataAnnotations.Schema;

namespace Storages.Providers.EntityFramework.Models;

public class Supply : BaseModel
{
    public DateTime Date { get; set; }
    
    [ForeignKey(nameof(Warehouse))]
    public string WarehouseGuid { get; set; } = null!;

    public virtual Warehouse? Warehouse { get; set; }

    public string ExternalStoreGuid { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}