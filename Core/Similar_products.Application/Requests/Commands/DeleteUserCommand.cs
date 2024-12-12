using MediatR;

namespace Similar_products.Application.Requests.Commands;

public record DeleteUserCommand(Guid Id) : IRequest<bool>;
