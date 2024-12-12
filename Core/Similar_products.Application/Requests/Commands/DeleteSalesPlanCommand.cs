using MediatR;

namespace Similar_products.Application.Requests.Commands;

public record DeleteSalesPlanCommand(Guid Id) : IRequest<bool>;
