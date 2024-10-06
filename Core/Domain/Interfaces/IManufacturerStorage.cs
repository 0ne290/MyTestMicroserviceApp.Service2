using Domain.Entities;
using FluentResults;

namespace Domain.Interfaces;

public interface IManufacturerStorage
{
    Task<ICollection<Manufacturer>> GetAll();
    
    Task<Result> Insert(Manufacturer manufacturer);
    
    Task<Result> Update(Manufacturer manufacturer);
}