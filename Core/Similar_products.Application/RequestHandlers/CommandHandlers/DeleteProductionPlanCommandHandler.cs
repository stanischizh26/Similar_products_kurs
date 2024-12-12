using MediatR;
using Similar_products.Domain.Abstractions;
using Similar_products.Application.Requests.Commands;

namespace Similar_products.Application.RequestHandlers.CommandHandlers;

public class DeleteProductionPlanCommandHandler(IProductionPlanRepository repository) : IRequestHandler<DeleteProductionPlanCommand, bool>
{
	private readonly IProductionPlanRepository _repository = repository;

	public async Task<bool> Handle(DeleteProductionPlanCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository.GetById(request.Id, trackChanges: false);

        if (entity is null)
        {
            return false;
        }

        _repository.Delete(entity);
        await _repository.SaveChanges();

        return true;
	}
}
