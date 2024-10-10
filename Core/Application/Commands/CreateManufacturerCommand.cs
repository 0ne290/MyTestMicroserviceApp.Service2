using FluentResults;
using MediatR;

namespace Application.Commands;

public class CreateManufacturerCommand : IRequest<Result>
{
    public required string Address { get; init; }
    
    public required string Name { get; init; }
}