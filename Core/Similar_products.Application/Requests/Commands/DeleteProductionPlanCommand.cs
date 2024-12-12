using MediatR;

namespace Similar_products.Application.Requests.Commands;

public record DeleteProductionPlanCommand(Guid Id) : IRequest<bool>;
