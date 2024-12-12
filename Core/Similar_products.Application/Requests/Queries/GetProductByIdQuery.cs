using MediatR;
using Similar_products.Application.Dtos;

namespace Similar_products.Application.Requests.Queries;

public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto?>;
