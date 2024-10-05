namespace Domain.Entities;

public class Warehouse(string guid, string address, (double Longitude, double Latitude) geolocation) : BaseEntity(guid)
{
    public string Address { get; } = address;

    public (double Longitude, double Latitude) Geolocation { get; } = geolocation;
}