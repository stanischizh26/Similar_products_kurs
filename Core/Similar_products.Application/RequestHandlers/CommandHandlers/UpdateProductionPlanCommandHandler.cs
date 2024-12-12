using MediatR;
using AutoMapper;
using Similar_products.Domain.Abstractions;
using Similar_products.Application.Requests.Commands;

namespace Similar_products.Application.RequestHandlers.CommandHandlers;

public class UpdateProductionPlanCommandHandler : IRequestHandler<UpdateProductionPlanCommand, bool>
{
	private readonly IProductionPlanRepository _repository;
	private readonly IMapper _mapper;

	public UpdateProductionPlanCommandHandler(IProductionPlanRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<bool> Handle(UpdateProductionPlanCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository.GetById(request.ProductionPlan.Id, trackChanges: true);

        if (entity is null)
        {
            return false;
        }

		_mapper.Map(request.ProductionPlan, entity);

		_repository.Update(entity);
		await _repository.SaveChanges();

		return true;
	}
}
