using FluentResults;
using MediatR;

namespace Application.Commands;

public class CreateSupplyCommand : IRequest<Result>
{
    public required string ExternalStoreGuid { get; init; }
    
    public required IReadOnlyCollection<string> ProductGuids { get; init; }
}