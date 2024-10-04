namespace Domain.Models;

public class Product : BaseModel
{
    public required Warehouse Warehouse { get; set; }
    
    public required Article Article { get; set; }
    
    public required DateTime ReceiptDate { get; set; }
    
    public Supply? Supply { get; set; }
}