using Domain.Entities;
using FluentResults;

namespace Domain.Interfaces;

public interface IProductStorage
{
    Task<IEnumerable<Product>> GetAll();
    
    Task<IEnumerable<Product>> GetAllBySupplyGuid(string supplyGuid);
    
    Task<Result> Insert(Product product);
    
    Task<Result> Update(Product product);
}