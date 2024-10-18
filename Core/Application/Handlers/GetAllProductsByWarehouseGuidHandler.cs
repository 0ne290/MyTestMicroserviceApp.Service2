using Application.Commands;
using Application.Dtos;
using Application.Mappers;
using Domain.Interfaces;
using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.Handlers;

public class GetAllProductsByWarehouseGuidHandler : IRequestHandler<GetAllProductsByWarehouseGuidCommand, Result<IEnumerable<ProductDto>>>
{
    public GetAllProductsByWarehouseGuidHandler(IProductStorage productStorage, IValidator<GetAllProductsByWarehouseGuidCommand> requestValidator)
    {
        _productStorage = productStorage;
        _requestValidator = requestValidator;
    }

    public async Task<Result<IEnumerable<ProductDto>>> Handle(GetAllProductsByWarehouseGuidCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _requestValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return Result.Fail(validationResult.ToString(" "));

        var products = (await _productStorage.GetAllByWarehouseGuid(request.WarehouseGuid)).ToList();
        var ret = new List<ProductDto>(products.Count);
        foreach (var product in products)
            ret.Add(await ProductMapper.EntityToDto(product));
        
        return Result.Ok(ret.AsEnumerable());
    }

    private readonly IProductStorage _productStorage;

    private readonly IValidator<GetAllProductsByWarehouseGuidCommand> _requestValidator;
}