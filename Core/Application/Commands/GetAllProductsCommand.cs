using Application.Dtos;
using MediatR;

namespace Application.Commands;

public class GetAllProductsCommand : IRequest<IEnumerable<ProductDto>>;