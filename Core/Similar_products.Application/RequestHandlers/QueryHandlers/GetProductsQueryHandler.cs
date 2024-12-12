using MediatR;
using AutoMapper;
using Similar_products.Application.Dtos;
using Similar_products.Domain.Abstractions;
using Similar_products.Application.Requests.Queries;

namespace Similar_products.Application.RequestHandlers.QueryHandlers;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, PageResult<ProductDto>>
{
	private readonly IProductRepository _repository;
	private readonly IMapper _mapper;

	public GetProductsQueryHandler(IProductRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

    //public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken) => 
    //	_mapper.Map<IEnumerable<ProductDto>>(await _repository.Get(trackChanges: false));

    public async Task<PageResult<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
	{
		var totalItems = await _repository.CountAsync(request.Name);
		var products = await _repository.GetPageAsync(request.Page, request.PageSize, request.Name);

		var items = _mapper.Map<IEnumerable<ProductDto>>(products);
		return new PageResult<ProductDto>(items, totalItems, request.Page, request.PageSize);

	}
}
