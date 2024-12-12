using MediatR;
using AutoMapper;
using Similar_products.Domain.Abstractions;
using Similar_products.Application.Requests.Queries;
using Similar_products.Application.Dtos;

namespace Similar_products.Application.RequestHandlers.QueryHandlers;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken) =>
        _mapper.Map<UserDto>(await _repository.GetById(request.Id, trackChanges: false));
}
