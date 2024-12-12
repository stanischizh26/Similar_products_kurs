using MediatR;
using AutoMapper;
using Similar_products.Domain.Abstractions;
using Similar_products.Domain.Entities;
using Similar_products.Application.Dtos;
using Similar_products.Application.Requests.Queries;

namespace Similar_products.Application.RequestHandlers.QueryHandlers;

public class GetUsersAllQueryHandler : IRequestHandler<GetUsersAllQuery, IEnumerable<User>>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public GetUsersAllQueryHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<User>> Handle(GetUsersAllQuery request, CancellationToken cancellationToken)
    {
        var users = _mapper.Map<IEnumerable<User>>(await _repository.Get(trackChanges: false, request.UserName));

        return users;
    }
}
