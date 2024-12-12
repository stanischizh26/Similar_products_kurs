using MediatR;
using AutoMapper;
using Similar_products.Application.Dtos;
using Similar_products.Domain.Abstractions;
using Similar_products.Application.Requests.Queries;

namespace Similar_products.Application.RequestHandlers.QueryHandlers;

public class GetSalesPlansQueryHandler : IRequestHandler<GetSalesPlansQuery, PageResult<SalesPlanDto>>
{
	private readonly ISalesPlanRepository _repository;
	private readonly IMapper _mapper;

	public GetSalesPlansQueryHandler(ISalesPlanRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<PageResult<SalesPlanDto>> Handle(GetSalesPlansQuery request, CancellationToken cancellationToken)
    {
        var totalItems = await _repository.CountAsync(request.Name);
        var sales = await _repository.GetPageAsync(request.Page, request.PageSize, request.Name);

        var items = _mapper.Map<IEnumerable<SalesPlanDto>>(sales);
        return new PageResult<SalesPlanDto>(items, totalItems, request.Page, request.PageSize);
    }
}
