using Domain.Entities;
using FluentResults;

namespace Domain.Interfaces;

public interface IManufacturerStorage
{
    Task<IEnumerable<Manufacturer>> GetAll();
    
    Task<Manufacturer> GetByGuid(string guid);
    
    Task<Result> Insert(Manufacturer manufacturer);
    
    Task<Result> Update(Manufacturer manufacturer);
}