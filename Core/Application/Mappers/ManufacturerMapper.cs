using Application.Dtos;
using Domain.Entities;

namespace Application.Mappers;

public static class ManufacturerMapper
{
    public static ManufacturerDto EntityToDto(Manufacturer entity) =>
        new() { Guid = entity.Guid, Address = entity.Address, Name = entity.Name };
}