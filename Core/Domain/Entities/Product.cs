namespace Domain.Entities;

public class Product(string guid, string name, Lazy<Task<Manufacturer>> manufacturer, DateTime receiptDate,
    Lazy<Task<Warehouse>> warehouse) : BaseEntity(guid)
{
    public string Name { get; set; } = name;

    public Lazy<Task<Manufacturer>> Manufacturer { get; set; } = manufacturer;

    public DateTime ReceiptDate { get; } = receiptDate;

    public Lazy<Task<Warehouse>> Warehouse{ get; } = warehouse;
}