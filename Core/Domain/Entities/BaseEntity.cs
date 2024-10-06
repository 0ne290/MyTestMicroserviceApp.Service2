namespace Domain.Entities;

public abstract class BaseEntity(string guid)
{
    protected bool Equals(BaseEntity other)
    {
        return Guid == other.Guid;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
            return false;
        if (ReferenceEquals(this, obj))
            return true;
        
        return obj.GetType() == GetType() && Equals((BaseEntity)obj);
    }

    public override int GetHashCode()
    {
        return Guid.GetHashCode();
    }

    public string Guid { get; } = guid;
}