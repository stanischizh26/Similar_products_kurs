using MediatR;
using Similar_products.Application.Dtos;

namespace Similar_products.Application.Requests.Commands;

public record UpdateProductCommand(ProductForUpdateDto Product) : IRequest<bool>;
