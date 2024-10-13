using Application.Commands;
using Domain.Entities;
using Domain.Interfaces;
using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.Handlers;

public class CreateManufacturerHandler : IRequestHandler<CreateManufacturerCommand, Result>
{
    public CreateManufacturerHandler(IManufacturerStorage manufacturerStorage, IValidator<CreateManufacturerCommand> requestValidator)
    {
        _manufacturerStorage = manufacturerStorage;
        _requestValidator = requestValidator;
    }

    public async Task<Result> Handle(CreateManufacturerCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _requestValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return Result.Fail(validationResult.ToString(" "));
        
        await _manufacturerStorage.Insert(new Manufacturer(Guid.NewGuid().ToString(), request.Address, request.Name));
        return Result.Ok();
    }

    private readonly IManufacturerStorage _manufacturerStorage;

    private readonly IValidator<CreateManufacturerCommand> _requestValidator;
}
