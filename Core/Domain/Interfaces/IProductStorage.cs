using Domain.Entities;
using FluentResults;

namespace Domain.Interfaces;

public interface IProductStorage
{
    Task<Result> Insert(Product product);
    
    Task<Result> Update(Product product);
}