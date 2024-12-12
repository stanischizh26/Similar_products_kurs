using MediatR;
using AutoMapper;
using Similar_products.Application.Dtos;
using Similar_products.Domain.Abstractions;
using Similar_products.Application.Requests.Queries;

namespace Similar_products.Application.RequestHandlers.QueryHandlers;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto?>
{
	private readonly IProductRepository _repository;
	private readonly IMapper _mapper;

	public GetProductByIdQueryHandler(IProductRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<ProductDto>(await _repository.GetById(request.Id, trackChanges: false));
}
