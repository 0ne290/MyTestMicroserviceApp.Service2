using Newtonsoft.Json;

namespace Application.Dtos;

public class SupplyDto : BaseDto
{
    [JsonProperty(Required = Required.Always, PropertyName = "productGuids")]
    public required IReadOnlyCollection<string> ProductGuids { get; init; }
    
    [JsonProperty(Required = Required.Always, PropertyName = "date")]
    public required DateTime Date { get; init; }

    [JsonProperty(Required = Required.Always, PropertyName = "externalStoreGuid")]
    public required string ExternalStoreGuid { get; set; }
}