using FluentResults;
using MediatR;
using Newtonsoft.Json;

namespace Application.Commands;

public class CreateManufacturerCommand : IRequest<Result>
{
    [JsonProperty(Required = Required.Always, PropertyName = "address")]
    public required string Address { get; init; }
    
    [JsonProperty(Required = Required.Always, PropertyName = "name")]
    public required string Name { get; init; }
}