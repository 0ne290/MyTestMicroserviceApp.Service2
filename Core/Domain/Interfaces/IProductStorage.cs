using Domain.Entities;
using FluentResults;

namespace Domain.Interfaces;

public interface IProductStorage
{
    Task<ICollection<Product>> GetAll();
    
    Task<Result> Insert(Product product);
    
    Task<Result> Update(Product product);
}