using Domain.Entities;
using FluentResults;

namespace Domain.Interfaces;

public interface IManufacturerStorage
{
    Task<IEnumerable<Manufacturer>> GetAll();
    
    Task<Manufacturer> GetByGuid(string guid);
    
    Task<bool> ExistsByGuid(string guid);
    
    Task<bool> ExistsByAddress(string address);
    
    Task<bool> ExistsByName(string name);
    
    Task<Result> Insert(Manufacturer manufacturer);
    
    Task<Result> Update(Manufacturer manufacturer);
}