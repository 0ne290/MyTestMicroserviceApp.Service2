using Newtonsoft.Json;

namespace Application.Dtos;

public class ProductDto : BaseDto
{
    [JsonProperty(Required = Required.Always, PropertyName = "name")]
    public required string Name { get; init; }

    [JsonProperty(Required = Required.Always, PropertyName = "manufacturerGuid")]
    public required string ManufacturerGuid { get; init; }

    [JsonProperty(Required = Required.Always, PropertyName = "recieptDate")]
    public required DateTime ReceiptDate { get; init; }

    [JsonProperty(Required = Required.Always, PropertyName = "warehouseGuid")]
    public required string WarehouseGuid { get; init; }
    
    [JsonProperty(Required = Required.AllowNull, PropertyName = "supplyGuid")]
    public required string? SupplyGuid { get; init; }
}