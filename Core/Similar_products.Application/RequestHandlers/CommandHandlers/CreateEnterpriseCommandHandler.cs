using MediatR;
using AutoMapper;
using Similar_products.Domain.Entities;
using Similar_products.Domain.Abstractions;
using Similar_products.Application.Requests.Commands;

namespace Similar_products.Application.RequestHandlers.CommandHandlers;

public class CreateEnterpriseCommandHandler : IRequestHandler<CreateEnterpriseCommand>
{
	private readonly IEnterpriseRepository _repository;
	private readonly IMapper _mapper;

	public CreateEnterpriseCommandHandler(IEnterpriseRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task Handle(CreateEnterpriseCommand request, CancellationToken cancellationToken)
	{
		await _repository.Create(_mapper.Map<Enterprise>(request.Enterprise));
		await _repository.SaveChanges();
	}
}
