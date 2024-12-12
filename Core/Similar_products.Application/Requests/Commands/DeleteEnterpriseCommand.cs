using MediatR;

namespace Similar_products.Application.Requests.Commands;

public record DeleteEnterpriseCommand(Guid Id) : IRequest<bool>;
