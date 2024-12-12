using MediatR;
using AutoMapper;
using Similar_products.Application.Dtos;
using Similar_products.Domain.Abstractions;
using Similar_products.Application.Requests.Queries;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Similar_products.Application.RequestHandlers.QueryHandlers;

public class GetEnterprisesAllQueryHandler : IRequestHandler<GetEnterprisesAllQuery, IEnumerable<EnterpriseDto>>
{
    private readonly IEnterpriseRepository _repository;
    private readonly IMapper _mapper;

    public GetEnterprisesAllQueryHandler(IEnterpriseRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EnterpriseDto>> Handle(GetEnterprisesAllQuery request, CancellationToken cancellationToken) =>
        _mapper.Map<IEnumerable<EnterpriseDto>>(await _repository.Get(false));

}
