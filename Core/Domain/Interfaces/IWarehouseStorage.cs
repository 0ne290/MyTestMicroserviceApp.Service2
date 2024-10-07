using Domain.Entities;
using FluentResults;

namespace Domain.Interfaces;

public interface IWarehouseStorage
{
    Task<IEnumerable<Warehouse>> GetAll();
    
    Task<Result> Insert(Warehouse warehouse);
    
    Task<Result> Update(Warehouse warehouse);
}