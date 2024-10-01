using Domain.Interfaces;
using Domain.Models;
using FluentResults;

namespace Storages.Providers.Default;

public class AnyModelStorage : IAnyModelStorage
{
    public Result Add(AnyModel model)
    {
        lock (Locker)
        {
            return !Models.Add(model) ? Result.Fail($"The model {model.Guid} is already in the storage.") : Result.Ok();
        }
    }

    private static readonly HashSet<AnyModel> Models = new(new AnyModel[]
    {
        new("Qdr"), new("jhr"), new("we45"), new("wAg"), new("75_-j"), new("WS2uhb!"),
        new("fffffffffff")
    });

    private static readonly object Locker = new();
}