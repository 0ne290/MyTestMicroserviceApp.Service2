using Application.Dtos;
using FluentResults;
using MediatR;
using Newtonsoft.Json;

namespace Application.Commands;

public class GetNearestWarehouseByGeolocationCommand : IRequest<Result<WarehouseDto>>
{
    [JsonProperty(Required = Required.Always, PropertyName = "geolocationLongitude")]
    public required double GeolocationLongitude { get; init; }
    
    [JsonProperty(Required = Required.Always, PropertyName = "geolocationLatitude")]
    public required double GeolocationLatitude { get; init; }
}