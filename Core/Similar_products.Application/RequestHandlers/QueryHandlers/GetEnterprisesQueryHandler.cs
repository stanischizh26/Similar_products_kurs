using MediatR;
using AutoMapper;
using Similar_products.Application.Dtos;
using Similar_products.Domain.Abstractions;
using Similar_products.Application.Requests.Queries;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Similar_products.Application.RequestHandlers.QueryHandlers;

public class GetEnterprisesQueryHandler : IRequestHandler<GetEnterprisesQuery, PageResult<EnterpriseDto>>
{
	private readonly IEnterpriseRepository _repository;
	private readonly IMapper _mapper;

	public GetEnterprisesQueryHandler(IEnterpriseRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<PageResult<EnterpriseDto>> Handle(GetEnterprisesQuery request, CancellationToken cancellationToken)
	{
        var totalItems = await _repository.CountAsync(request.Name);
        var enterprises = await _repository.GetPageAsync(request.Page, request.PageSize, request.Name);

        var items = _mapper.Map<IEnumerable<EnterpriseDto>>(enterprises);
        return new PageResult<EnterpriseDto>(items, totalItems, request.Page, request.PageSize);
    }

}
