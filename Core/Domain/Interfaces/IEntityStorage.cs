using Domain.Entities;
using FluentResults;

namespace Domain.Interfaces;

public interface IEntityStorage<T> where T : BaseEntity
{
    Task<Result> Insert(T entity);
}