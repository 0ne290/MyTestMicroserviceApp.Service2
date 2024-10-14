using Application.Dtos;
using MediatR;

namespace Application.Commands;

public class GetAllSuppliesCommand : IRequest<IEnumerable<SupplyDto>>;