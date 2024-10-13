using FluentResults;
using MediatR;
using Newtonsoft.Json;

namespace Application.Commands;

public class CreateWarehouseCommand : IRequest<Result>
{
    [JsonProperty(Required = Required.Always, PropertyName = "address")]
    public required string Address { get; init; }
    
    [JsonProperty(Required = Required.Always, PropertyName = "geolocationLongitude")]
    public required double GeolocationLongitude { get; init; }
    
    [JsonProperty(Required = Required.Always, PropertyName = "geolocationLatitude")]
    public required double GeolocationLatitude { get; init; }
}