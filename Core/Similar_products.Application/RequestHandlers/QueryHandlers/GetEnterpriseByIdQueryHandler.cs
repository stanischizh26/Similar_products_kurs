using MediatR;
using AutoMapper;
using Similar_products.Application.Dtos;
using Similar_products.Domain.Abstractions;
using Similar_products.Application.Requests.Queries;

namespace Similar_products.Application.RequestHandlers.QueryHandlers;

public class GetEnterpriseByIdQueryHandler : IRequestHandler<GetEnterpriseByIdQuery, EnterpriseDto?>
{
	private readonly IEnterpriseRepository _repository;
	private readonly IMapper _mapper;

	public GetEnterpriseByIdQueryHandler(IEnterpriseRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<EnterpriseDto?> Handle(GetEnterpriseByIdQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<EnterpriseDto>(await _repository.GetById(request.Id, trackChanges: false));
}
