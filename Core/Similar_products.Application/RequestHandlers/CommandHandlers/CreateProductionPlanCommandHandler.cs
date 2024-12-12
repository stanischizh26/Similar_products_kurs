using MediatR;
using AutoMapper;
using Similar_products.Domain.Entities;
using Similar_products.Domain.Abstractions;
using Similar_products.Application.Requests.Commands;

namespace Similar_products.Application.RequestHandlers.CommandHandlers;

public class CreateProductionPlanCommandHandler : IRequestHandler<CreateProductionPlanCommand>
{
	private readonly IProductionPlanRepository _repository;
	private readonly IMapper _mapper;

	public CreateProductionPlanCommandHandler(IProductionPlanRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task Handle(CreateProductionPlanCommand request, CancellationToken cancellationToken)
	{
		await _repository.Create(_mapper.Map<ProductionPlan>(request.ProductionPlan));
		await _repository.SaveChanges();
	}
}
