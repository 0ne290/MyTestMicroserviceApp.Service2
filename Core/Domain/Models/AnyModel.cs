using Domain.Constants;
using FluentResults;

namespace Domain.Models;

public class AnyModel : BaseModel
{
    // Constructor for the EF
    private AnyModel() : base(string.Empty, DateTime.UnixEpoch)
    {
    }

    public AnyModel(string anyItem)
    {
        Status = StatusesOfAnyModel.AwaitingCallOfMethod1;
        AnyItem = anyItem;
        AnyRelatedItem1 = null;
        AnyRelatedItem2 = null;
        AnyRelatedItem3 = null;
        AnyRelatedItem4 = null;
    }

    public Result Delete()
    {
        if (DeleteDate != null)
            return Result.Fail($"Model {Guid}: already deleted.");

        DeleteDate = DateTime.Now;

        return Result.Ok();
    }

    public Result Method1(string anyRelatedItem1, string? anyRelatedItem2 = null)
    {
        if (Status != StatusesOfAnyModel.AwaitingCallOfMethod1)
            return Result.Fail($"Model {Guid}: to call Method1 the status value must be \"AwaitingCallOfMethod1\".");
        if (DeleteDate != null)
            return Result.Fail($"Model {Guid}: Method1 can only be called if the model is not deleted.");

        Status = StatusesOfAnyModel.AwaitingCallOfMethod2;
        AnyRelatedItem1 = anyRelatedItem1;
        AnyRelatedItem2 = anyRelatedItem2;

        return Result.Ok();
    }

    public Result Method2(string anyRelatedItem3, string? anyRelatedItem4 = null)
    {
        if (Status != StatusesOfAnyModel.AwaitingCallOfMethod1)
            return Result.Fail($"Model {Guid}: to call Method1 the status value must be \"AwaitingCallOfMethod2\".");
        if (DeleteDate != null)
            return Result.Fail($"Model {Guid}: Method1 can only be called if the model is not deleted.");

        Status = StatusesOfAnyModel.AllMethodsAreCalled;
        AnyRelatedItem3 = anyRelatedItem3;
        AnyRelatedItem4 = anyRelatedItem4;

        return Result.Ok();
    }

    public string Status { get; private set; }

    public string AnyItem { get; set; }

    public string? AnyRelatedItem1 { get; private set; }

    public string? AnyRelatedItem2 { get; private set; }

    public string? AnyRelatedItem3 { get; private set; }

    public string? AnyRelatedItem4 { get; private set; }
}