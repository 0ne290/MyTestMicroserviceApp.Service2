using Application.Commands;
using Application.Dtos;
using Application.Mappers;
using Domain.Interfaces;
using MediatR;

namespace Application.Handlers;

public class GetAllManufacturersHandler : IRequestHandler<GetAllManufacturersCommand, IEnumerable<ManufacturerDto>>
{
    public GetAllManufacturersHandler(IManufacturerStorage manufacturerStorage)
    {
        _manufacturerStorage = manufacturerStorage;
    }

    public async Task<IEnumerable<ManufacturerDto>> Handle(GetAllManufacturersCommand _, CancellationToken __) =>
        (await _manufacturerStorage.GetAll()).Select(ManufacturerMapper.EntityToDto);

    private readonly IManufacturerStorage _manufacturerStorage;
}