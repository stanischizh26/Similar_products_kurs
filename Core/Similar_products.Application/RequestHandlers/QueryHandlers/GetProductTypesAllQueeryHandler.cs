using MediatR;
using AutoMapper;
using Similar_products.Application.Dtos;
using Similar_products.Domain.Abstractions;
using Similar_products.Application.Requests.Queries;

namespace Similar_products.Application.RequestHandlers.QueryHandlers;

public class GetProductTypesAllQueryHandler : IRequestHandler<GetProductTypesAllQuery, IEnumerable<ProductTypeDto>>
{
    private readonly IProductTypeRepository _repository;
    private readonly IMapper _mapper;

    public GetProductTypesAllQueryHandler(IProductTypeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductTypeDto>> Handle(GetProductTypesAllQuery request, CancellationToken cancellationToken) =>
        _mapper.Map<IEnumerable<ProductTypeDto>>(await _repository.Get(false));
}
