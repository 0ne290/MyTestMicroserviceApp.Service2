using Application.Commands;
using Domain.Entities;
using Domain.Interfaces;
using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.Handlers;

public class CreateSupplyHandler : IRequestHandler<CreateSupplyCommand, Result>
{
    public CreateSupplyHandler(ISupplyStorage supplyStorage, IProductStorage productStorage, IValidator<CreateSupplyCommand> requestValidator)
    {
        _supplyStorage = supplyStorage;
        _productStorage = productStorage;
        _requestValidator = requestValidator;
    }

    public async Task<Result> Handle(CreateSupplyCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _requestValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return Result.Fail(validationResult.ToString(" "));

        var products =
            new Lazy<Task<IEnumerable<Product>>>(async () => await _productStorage.GetAllByGuids(request.ProductGuids));
        await _supplyStorage.Insert(new Supply(Guid.NewGuid().ToString(), DateTime.Now, request.ExternalStoreGuid,
            products));
        return Result.Ok();
    }

    private readonly ISupplyStorage _supplyStorage;
    
    private readonly IProductStorage _productStorage;

    private readonly IValidator<CreateSupplyCommand> _requestValidator;
}
