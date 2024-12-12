using MediatR;
using AutoMapper;
using Similar_products.Application.Dtos;
using Similar_products.Domain.Abstractions;
using Similar_products.Application.Requests.Queries;

namespace Similar_products.Application.RequestHandlers.QueryHandlers;

public class GetProductionPlanByIdQueryHandler : IRequestHandler<GetProductionPlanByIdQuery, ProductionPlanDto?>
{
	private readonly IProductionPlanRepository _repository;
	private readonly IMapper _mapper;

	public GetProductionPlanByIdQueryHandler(IProductionPlanRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<ProductionPlanDto?> Handle(GetProductionPlanByIdQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<ProductionPlanDto>(await _repository.GetById(request.Id, trackChanges: false));
}
