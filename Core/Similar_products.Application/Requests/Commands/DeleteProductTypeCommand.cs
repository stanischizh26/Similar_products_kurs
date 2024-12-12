using MediatR;

namespace Similar_products.Application.Requests.Commands;

public record DeleteProductTypeCommand(Guid Id) : IRequest<bool>;
