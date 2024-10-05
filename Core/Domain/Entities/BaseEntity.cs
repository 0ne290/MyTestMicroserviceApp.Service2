namespace Domain.Entities;

public abstract class BaseEntity(string guid)
{
    public override int GetHashCode() => Guid.GetHashCode();

    public string Guid { get; } = guid;
}