using MediatR;
using AutoMapper;
using Similar_products.Domain.Abstractions;
using Similar_products.Application.Requests.Commands;

namespace Similar_products.Application.RequestHandlers.CommandHandlers;

public class UpdateEnterpriseCommandHandler : IRequestHandler<UpdateEnterpriseCommand, bool>
{
	private readonly IEnterpriseRepository _repository;
	private readonly IMapper _mapper;

	public UpdateEnterpriseCommandHandler(IEnterpriseRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<bool> Handle(UpdateEnterpriseCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository.GetById(request.Enterprise.Id, trackChanges: true);

        if (entity is null)
        {
            return false;
        }

		_mapper.Map(request.Enterprise, entity);

		_repository.Update(entity);
		await _repository.SaveChanges();

		return true;
	}
}
