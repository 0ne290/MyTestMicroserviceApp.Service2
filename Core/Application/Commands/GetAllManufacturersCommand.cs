using Application.Dtos;
using MediatR;

namespace Application.Commands;

public class GetAllManufacturersCommand : IRequest<IEnumerable<ManufacturerDto>>;