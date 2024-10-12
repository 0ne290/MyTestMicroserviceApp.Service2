using Application.Commands;
using Domain.Interfaces;
using FluentResults;
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
        var validationResult = await _requestValidator.ValidateAsync(request);
        
        if (validationResult.IsValid)
        {
            await _warehouseStorage.Insert(new Warehouse(Guid.NewGuid().ToString(), request.Address, (request.GeolocationLongitude, request.GeolocationLatitude)));
            return Result.Ok();
        }

        return Result.Fail(validationResult.ToString(" "));
    }

    private readonly IWarehouseStorage _warehouseStorage;

    private readonly IValidator<CreateWarehouseCommand> _requestValidator
}
