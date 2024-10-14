using Domain.Entities;
using FluentResults;

namespace Domain.Interfaces;

public interface IWarehouseStorage
{
    Task<IEnumerable<Warehouse>> GetAll();
    
    Task<Warehouse> GetByGuid(string guid);

    Task<bool> ExistsByGuid(string guid);
    
    Task<bool> ExistsByGeolocation((double Longitude, double Latitude) geolocation);
    
    Task<bool> ExistsByAddress(string address);
    
    Task<Result> Insert(Warehouse warehouse);
}