using Newtonsoft.Json;

namespace Application.Dtos;

public class ManufacturerDto : BaseDto
{
    [JsonProperty(Required = Required.Always, PropertyName = "address")]
    public required string Address { get; init; }
    
    [JsonProperty(Required = Required.Always, PropertyName = "name")]
    public required string Name { get; init; }
}