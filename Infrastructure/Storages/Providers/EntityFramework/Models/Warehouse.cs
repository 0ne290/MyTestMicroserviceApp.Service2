using Microsoft.EntityFrameworkCore;

namespace Storages.Providers.EntityFramework.Models;

[Index(nameof(Address), IsUnique = true)]
[Index(nameof(GeolocationLongitude), nameof(GeolocationLatitude), IsUnique = true)]
public class Warehouse : BaseModel
{
    public string Address { get; set; } = null!;
    
    public double GeolocationLongitude { get; set; }
    
    public double GeolocationLatitude { get; set; }
}