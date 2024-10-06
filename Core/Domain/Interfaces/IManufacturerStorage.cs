using Domain.Entities;
using FluentResults;

namespace Domain.Interfaces;

public interface IManufacturerStorage
{
    Task<Result> Insert(Manufacturer manufacturer);
    
    Task<Result> Update(Manufacturer manufacturer);
}