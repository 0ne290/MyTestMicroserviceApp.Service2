using Application.Dtos;
using FluentResults;
using MediatR;
using Newtonsoft.Json;

namespace Application.Commands;

public class GetAllProductsByWarehouseGuidCommand : IRequest<Result<IEnumerable<ProductDto>>>
{
    [JsonProperty(Required = Required.Always, PropertyName = "warehouseGuid")]
    public required string WarehouseGuid { get; init; }
}