using Application.Commands;
using Application.Dtos;
using Application.Mappers;
using Domain.Interfaces;
using MediatR;

namespace Application.Handlers;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsCommand, IEnumerable<ProductDto>>
{
    public GetAllProductsHandler(IProductStorage productStorage)
    {
        _productStorage = productStorage;
    }

    public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsCommand _, CancellationToken __)
    {
        var entities = (await _productStorage.GetAll()).ToList();
        var dtos = new List<ProductDto>(entities.Count);
        foreach (var entity in entities)
            dtos.Add(await ProductMapper.EntityToDto(entity));

        return dtos;
    }

    private readonly IProductStorage _productStorage;
}