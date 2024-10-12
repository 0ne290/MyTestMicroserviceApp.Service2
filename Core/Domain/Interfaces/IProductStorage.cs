using Domain.Entities;
using FluentResults;

namespace Domain.Interfaces;

public interface IProductStorage
{
    Task<IEnumerable<Product>> GetAll();
    
    Task<IEnumerable<Product>> GetAllBySupplyGuid(string supplyGuid);
    
    Task<IEnumerable<Product>> GetAllByGuids(IEnumerable<string> guids);
    
    Task<Result> Insert(Product product);
    
    Task<Result> Update(Product product);
}