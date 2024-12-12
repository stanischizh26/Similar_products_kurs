using MediatR;

namespace Similar_products.Application.Requests.Commands;

public record DeleteProductCommand(Guid Id) : IRequest<bool>;
