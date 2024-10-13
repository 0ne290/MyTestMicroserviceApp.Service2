using FluentResults;
using MediatR;
using Newtonsoft.Json;

namespace Application.Commands;

public class CreateProductCommand : IRequest<Result>
{
    [JsonProperty(Required = Required.Always, PropertyName = "name")]
    public required string Name { get; init; }
    
    [JsonProperty(Required = Required.Always, PropertyName = "manufacturerGuid")]
    public required string ManufacturerGuid { get; init; }
    
    [JsonProperty(Required = Required.Always, PropertyName = "warehouseGuid")]
    public required string WarehouseGuid { get; init; }
}