using MediatR;
using AutoMapper;
using Similar_products.Domain.Abstractions;
using Similar_products.Application.Requests.Queries;
using Similar_products.Domain.Entities;
using Similar_products.Application.Dtos;
using Similar_products.Application;

namespace Similar_products.Application.RequestHandlers.QueryHandlers;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PageResult<UserDto>>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public GetUsersQueryHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PageResult<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var totalItems = await _repository.CountAsync(request.Name);
        var users = await _repository.GetPageAsync(request.Page, request.PageSize, request.Name);

        var items = _mapper.Map<IEnumerable<UserDto>>(users);
        return new PageResult<UserDto>(items, totalItems, request.Page, request.PageSize);
    }
}
