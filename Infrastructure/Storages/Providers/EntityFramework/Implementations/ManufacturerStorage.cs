using Domain.Entities;
using Domain.Interfaces;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Storages.Providers.EntityFramework.Mappers;

namespace Storages.Providers.EntityFramework.Implementations;

public class ManufacturerStorage(Service2Context dbContext) : IManufacturerStorage
{
    public async Task<ICollection<Manufacturer>> GetAll() =>
        await Task.FromResult(_dbContext.Manufacturers.Select(ManufacturerMapper.ModelToEntity).ToList());
    
    public async Task<Result> Insert(Manufacturer manufacturer)
    {
        await _dbContext.Manufacturers.AddAsync(ManufacturerMapper.EntityToModel(manufacturer));
        await _dbContext.SaveChangesAsync();
        
        return Result.Ok();
    }

    public async Task<Result> Update(Manufacturer manufacturer)
    {
        _dbContext.Update(ManufacturerMapper.EntityToModel(manufacturer));
        await _dbContext.SaveChangesAsync();
        
        return Result.Ok();
    }
    
    private readonly Service2Context _dbContext = dbContext;
}