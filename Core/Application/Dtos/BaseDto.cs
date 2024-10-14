using Newtonsoft.Json;

namespace Application.Dtos;

public abstract class BaseDto
{
    [JsonProperty(Required = Required.Always, PropertyName = "guid")]
    public required string Guid { get; init; }
}