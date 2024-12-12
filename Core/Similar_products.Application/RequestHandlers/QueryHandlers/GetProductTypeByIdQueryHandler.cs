using MediatR;
using AutoMapper;
using Similar_products.Application.Dtos;
using Similar_products.Domain.Abstractions;
using Similar_products.Application.Requests.Queries;

namespace Similar_products.Application.RequestHandlers.QueryHandlers;

public class GetProductTypeByIdQueryHandler : IRequestHandler<GetProductTypeByIdQuery, ProductTypeDto?>
{
	private readonly IProductTypeRepository _repository;
	private readonly IMapper _mapper;

	public GetProductTypeByIdQueryHandler(IProductTypeRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<ProductTypeDto?> Handle(GetProductTypeByIdQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<ProductTypeDto>(await _repository.GetById(request.Id, trackChanges: false));
}
