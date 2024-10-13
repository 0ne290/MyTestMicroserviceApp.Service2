using Application.Commands;
using Domain.Entities;
using Domain.Interfaces;
using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.Handlers;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, Result>
{
    public CreateProductHandler(IProductStorage productStorage, IManufacturerStorage manufacturerStorage,
        IWarehouseStorage warehouseStorage, IValidator<CreateProductCommand> requestValidator)
    {
        _productStorage = productStorage;
        _manufacturerStorage = manufacturerStorage;
        _warehouseStorage = warehouseStorage;
        _requestValidator = requestValidator;
    }

    public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _requestValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return Result.Fail(validationResult.ToString(" "));

        var manufacturer =
            new Lazy<Task<Manufacturer>>(async () => await _manufacturerStorage.GetByGuid(request.ManufacturerGuid));
        var warehouse =
            new Lazy<Task<Warehouse>>(async () => await _warehouseStorage.GetByGuid(request.WarehouseGuid));
        await _productStorage.Insert(new Product(Guid.NewGuid().ToString(), request.Name, manufacturer, DateTime.Now, warehouse));
        return Result.Ok();
    }

    private readonly IProductStorage _productStorage;
    
    private readonly IManufacturerStorage _manufacturerStorage;
    
    private readonly IWarehouseStorage _warehouseStorage;

    private readonly IValidator<CreateProductCommand> _requestValidator;
}
