using Domain.Entities;
using FluentResults;

namespace Domain.Interfaces;

public interface IWarehouseStorage
{
    Task<IEnumerable<Warehouse>> GetAll();
    
    Task<Warehouse> GetByGuid(string guid);
    
    Task<Result> Insert(Warehouse warehouse);
    
    Task<Result> Update(Warehouse warehouse);
}