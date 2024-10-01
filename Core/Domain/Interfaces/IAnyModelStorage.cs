using Domain.Models;
using FluentResults;

namespace Domain.Interfaces;

public interface IAnyModelStorage
{
    Result Add(AnyModel model);
}