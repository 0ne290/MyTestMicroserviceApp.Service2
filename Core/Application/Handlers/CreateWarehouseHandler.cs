using Application.Commands;
using Domain.Interfaces;
using Domain.Validators;
using FluentResults;
using MediatR;

namespace Application.Handlers;

public class CreateWarehouseHandler : IRequestHandler<CreateWarehouseCommand, Result>
{
    public async Task<Result> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
    {
        var model = new AnyModel(request.AnyItem);
        var modelValidator = new AnyModelValidator();
        var validationResult = modelValidator.Validate(model);

        return await Task.FromResult(!validationResult.IsValid
            ? Result.Fail(validationResult.ToString("; "))
            : modelStorage.Add(model));
    }
}