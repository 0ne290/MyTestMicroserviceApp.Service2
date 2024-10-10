using FluentResults;
using MediatR;

namespace Application.Commands;

public class CreateProductCommand : IRequest<Result>
{
    public required string Name { get; init; }
    
    public required string ManufacturerGuid { get; init; }
    
    public required string WarehouseGuid { get; init; }
}