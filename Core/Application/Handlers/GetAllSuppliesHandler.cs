using Application.Commands;
using Application.Dtos;
using Application.Mappers;
using Domain.Interfaces;
using MediatR;

namespace Application.Handlers;

public class GetAllSuppliesHandler : IRequestHandler<GetAllSuppliesCommand, IEnumerable<SupplyDto>>
{
    public GetAllSuppliesHandler(ISupplyStorage supplyStorage)
    {
        _supplyStorage = supplyStorage;
    }

    public async Task<IEnumerable<SupplyDto>> Handle(GetAllSuppliesCommand _, CancellationToken __)
    {
        var entities = (await _supplyStorage.GetAll()).ToList();
        var dtos = new List<SupplyDto>(entities.Count);
        foreach (var entity in entities)
            dtos.Add(await SupplyMapper.EntityToDto(entity));

        return dtos;
    }

    private readonly ISupplyStorage _supplyStorage;
}