using FluentResults;
using MediatR;

namespace Application.Commands;

public class CreateWarehouseCommand : IRequest<Result>
{
    public required string Address { get; init; }
    
    public required double GeolocationLongitude { get; init; }
    
    public required double GeolocationLatitude { get; init; }
}