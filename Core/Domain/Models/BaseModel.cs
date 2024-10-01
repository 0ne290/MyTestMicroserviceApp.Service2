namespace Domain.Models;

public abstract class BaseModel
{
    protected BaseModel()
    {
        Guid = System.Guid.NewGuid().ToString();
        CreateDate = DateTime.Now;
    }

    // Base constructor called by constructors for the EF
    protected BaseModel(string guid, DateTime createDate)
    {
        Guid = guid;
        CreateDate = createDate;
    }

    public override int GetHashCode() => Guid.GetHashCode();

    public string Guid { get; private init; }

    public DateTime CreateDate { get; private init; }

    public DateTime? DeleteDate { get; protected set; } = null;
}