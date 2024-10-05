namespace Domain.Entities;

public class Manufacturer(string guid, string address, string name) : BaseEntity(guid)
{
    public string Address { get; init; } = address;

    public string Name { get; init; } = name;
}