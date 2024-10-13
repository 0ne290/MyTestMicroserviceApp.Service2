using FluentResults;
using MediatR;
using Newtonsoft.Json;

namespace Application.Commands;

public class CreateSupplyCommand : IRequest<Result>
{
    [JsonProperty(Required = Required.Always, PropertyName = "externalStoreGuid")]
    public required string ExternalStoreGuid { get; init; }
    
    [JsonProperty(Required = Required.Always, PropertyName = "productGuids")]
    public required IReadOnlyCollection<string> ProductGuids { get; init; }
}