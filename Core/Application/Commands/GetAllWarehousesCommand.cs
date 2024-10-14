using Application.Dtos;
using MediatR;

namespace Application.Commands;

public class GetAllWarehousesCommand : IRequest<IEnumerable<WarehouseDto>>;