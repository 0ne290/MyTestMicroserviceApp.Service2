using Microsoft.EntityFrameworkCore;

namespace Storages.Providers.EntityFramework.Models;

[PrimaryKey(nameof(Guid))]
public abstract class BaseModel
{
    public required string Guid { get; init; }
}