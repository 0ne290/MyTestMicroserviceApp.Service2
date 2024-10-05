using Microsoft.EntityFrameworkCore;

namespace Storages.Providers.EntityFramework.Models;

[Index(nameof(Address), IsUnique = true)]
[Index(nameof(Name), IsUnique = true)]
public class Manufacturer : BaseModel
{
    public string Address { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}