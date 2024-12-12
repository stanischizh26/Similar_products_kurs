using MediatR;
using AutoMapper;
using Similar_products.Application.Dtos;
using Similar_products.Domain.Abstractions;
using Similar_products.Application.Requests.Queries;

namespace Similar_products.Application.RequestHandlers.QueryHandlers;

public class GetProductionPlansQueryHandler : IRequestHandler<GetProductionPlansQuery, PageResult<ProductionPlanDto>>
{
	private readonly IProductionPlanRepository _repository;
	private readonly IMapper _mapper;

	public GetProductionPlansQueryHandler(IProductionPlanRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<PageResult<ProductionPlanDto>> Handle(GetProductionPlansQuery request, CancellationToken cancellationToken)
    {
        var totalItems = await _repository.CountAsync(request.Name);
        var plans = await _repository.GetPageAsync(request.Page, request.PageSize, request.Name);

        var items = _mapper.Map<IEnumerable<ProductionPlanDto>>(plans);
        return new PageResult<ProductionPlanDto>(items, totalItems, request.Page, request.PageSize);
    }
}
