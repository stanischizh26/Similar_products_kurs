using MediatR;
using AutoMapper;
using Similar_products.Application.Dtos;
using Similar_products.Domain.Abstractions;
using Similar_products.Application.Requests.Queries;

namespace Similar_products.Application.RequestHandlers.QueryHandlers;

public class GetProductTypesQueryHandler : IRequestHandler<GetProductTypesQuery, PageResult<ProductTypeDto>>
{
    private readonly IProductTypeRepository _repository;
    private readonly IMapper _mapper;

    public GetProductTypesQueryHandler(IProductTypeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PageResult<ProductTypeDto>> Handle(GetProductTypesQuery request, CancellationToken cancellationToken)
    {
        var totalItems = await _repository.CountAsync(request.Name);
        var products = await _repository.GetPageAsync(request.Page, request.PageSize, request.Name);

        var items = _mapper.Map<IEnumerable<ProductTypeDto>>(products);
        return new PageResult<ProductTypeDto>(items, totalItems, request.Page, request.PageSize);

    }
}
