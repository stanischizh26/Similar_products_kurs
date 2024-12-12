using MediatR;
using Similar_products.Application.Dtos;

namespace Similar_products.Application.Requests.Queries;

public record GetProductTypeByIdQuery(Guid Id) : IRequest<ProductTypeDto?>;
