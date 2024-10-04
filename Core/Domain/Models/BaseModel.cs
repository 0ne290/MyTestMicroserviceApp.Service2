namespace Domain.Models;

public abstract class BaseModel
{
    public override int GetHashCode() => Guid.GetHashCode();

    public required string Guid { get; init; }
}