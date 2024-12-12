using MediatR;
using AutoMapper;
using Similar_products.Domain.Entities;
using Similar_products.Domain.Abstractions;
using Similar_products.Application.Requests.Commands;

namespace Similar_products.Application.RequestHandlers.CommandHandlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        await _repository.Create(_mapper.Map<User>(request.User));
        await _repository.SaveChanges();
    }
}
