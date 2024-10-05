namespace Domain.Entities;

public class Manufacturer(string guid, string address, string name) : BaseEntity(guid)
{
    public string Address { get; } = address;

    public string Name { get; } = name;
}