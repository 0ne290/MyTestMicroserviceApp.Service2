namespace Domain.Entities;

public class Product(string guid, string name, Lazy<Manufacturer> manufacturer, DateTime receiptDate,
    Lazy<Warehouse> warehouse) : BaseEntity(guid)
{
    public string Name { get; } = name;

    public Lazy<Manufacturer> Manufacturer { get; } = manufacturer;

    public DateTime ReceiptDate { get; } = receiptDate;

    public Lazy<Warehouse> Warehouse { get; } = warehouse;
}