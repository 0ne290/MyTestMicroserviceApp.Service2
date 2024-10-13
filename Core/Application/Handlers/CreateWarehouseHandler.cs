using Application.Commands;
using Domain.Entities;
using Domain.Interfaces;
using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.Handlers;

public class CreateWarehouseHandler : IRequestHandler<CreateWarehouseCommand, Result>
{
    public CreateWarehouseHandler(IWarehouseStorage warehouseStorage, IValidator<CreateWarehouseCommand> requestValidator)
    {
        _warehouseStorage = warehouseStorage;
        _requestValidator = requestValidator;
    }

    public async Task<Result> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _requestValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return Result.Fail(validationResult.ToString(" "));
        
        await _warehouseStorage.Insert(new Warehouse(Guid.NewGuid().ToString(), request.Address, (request.GeolocationLongitude, request.GeolocationLatitude)));
        return Result.Ok();
    }

    private readonly IWarehouseStorage _warehouseStorage;

    private readonly IValidator<CreateWarehouseCommand> _requestValidator;
}
