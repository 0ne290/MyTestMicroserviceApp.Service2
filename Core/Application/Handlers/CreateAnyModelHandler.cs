using Application.Commands;
using Domain.Interfaces;
using Domain.Models;
using Domain.Validators;
using FluentResults;
using MediatR;

namespace Application.Handlers;

public class CreateAnyModelHandler(IAnyModelStorage modelStorage) : IRequestHandler<CreateAnyModelCommand, Result>
{
    public Task<Result> Handle(CreateAnyModelCommand request, CancellationToken cancellationToken)
    {
        var model = new AnyModel(request.AnyItem);
        var modelValidator = new AnyModelValidator();
        var validationResult = modelValidator.Validate(model);

        return Task.FromResult(!validationResult.IsValid
            ? Result.Fail(validationResult.ToString("; "))
            : modelStorage.Add(model));
    }
}