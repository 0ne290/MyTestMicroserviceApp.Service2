using Application.Commands;
using Application.Dtos;
using Application.Mappers;
using Domain.Interfaces;
using MediatR;

namespace Application.Handlers;

public class GetAllWarehousesHandler : IRequestHandler<GetAllWarehousesCommand, IEnumerable<WarehouseDto>>
{
    public GetAllWarehousesHandler(IWarehouseStorage warehouseStorage)
    {
        _warehouseStorage = warehouseStorage;
    }

    public async Task<IEnumerable<WarehouseDto>> Handle(GetAllWarehousesCommand _, CancellationToken __) =>
        (await _warehouseStorage.GetAll()).Select(WarehouseMapper.EntityToDto);

    private readonly IWarehouseStorage _warehouseStorage;
}