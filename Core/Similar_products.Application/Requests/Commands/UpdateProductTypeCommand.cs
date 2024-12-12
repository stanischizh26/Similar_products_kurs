using MediatR;
using Similar_products.Application.Dtos;

namespace Similar_products.Application.Requests.Commands;

public record UpdateProductTypeCommand(ProductTypeForUpdateDto ProductType) : IRequest<bool>;
