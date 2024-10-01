using FluentResults;
using MediatR;

namespace Application.Commands;

public class CreateAnyModelCommand : IRequest<Result>
{
    public required string AnyItem { get; init; }
}