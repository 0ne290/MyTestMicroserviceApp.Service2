namespace Domain.Entities;

public class Product(string guid, string name, Lazy<Task<Manufacturer>> manufacturer, DateTime receiptDate,
    Lazy<Task<Warehouse>> warehouse) : BaseEntity(guid)
{
    public string Name { get; } = name;

    public Lazy<Task<Manufacturer>> Manufacturer { get; } = manufacturer;

    public DateTime ReceiptDate { get; } = receiptDate;

    public Lazy<Task<Warehouse>> Warehouse{ get; } = warehouse;
}