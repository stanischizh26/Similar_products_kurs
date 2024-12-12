using MediatR;
using AutoMapper;
using Similar_products.Application.Dtos;
using Similar_products.Domain.Abstractions;
using Similar_products.Application.Requests.Queries;

namespace Similar_products.Application.RequestHandlers.QueryHandlers;

public class GetSalesPlanByIdQueryHandler : IRequestHandler<GetSalesPlanByIdQuery, SalesPlanDto?>
{
	private readonly ISalesPlanRepository _repository;
	private readonly IMapper _mapper;

	public GetSalesPlanByIdQueryHandler(ISalesPlanRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<SalesPlanDto?> Handle(GetSalesPlanByIdQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<SalesPlanDto>(await _repository.GetById(request.Id, trackChanges: false));
}
