using Domain.Entities;
using FluentResults;

namespace Domain.Interfaces;

public interface ISupplyStorage
{
    Task<IEnumerable<Supply>> GetAll();
    
    Task<Supply> GetByGuid(string guid);
    
    Task<Result> Insert(Supply supply);
    
    Task<Result> Update(Supply supply);
}