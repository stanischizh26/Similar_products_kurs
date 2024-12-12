using MediatR;
using AutoMapper;
using Similar_products.Domain.Abstractions;
using Similar_products.Application.Requests.Commands;

namespace Similar_products.Application.RequestHandlers.CommandHandlers;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.User.Id, trackChanges: true);

        if (entity is null)
        {
            return false;
        }

        _mapper.Map(request.User, entity);

        _repository.Update(entity);
        await _repository.SaveChanges();

        return true;
    }
}
