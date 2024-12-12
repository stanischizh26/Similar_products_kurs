using MediatR;
using AutoMapper;
using Similar_products.Domain.Entities;
using Similar_products.Domain.Abstractions;
using Similar_products.Application.Requests.Commands;

namespace Similar_products.Application.RequestHandlers.CommandHandlers;

public class CreateSalesPlanCommandHandler : IRequestHandler<CreateSalesPlanCommand>
{
	private readonly ISalesPlanRepository _repository;
	private readonly IMapper _mapper;

	public CreateSalesPlanCommandHandler(ISalesPlanRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task Handle(CreateSalesPlanCommand request, CancellationToken cancellationToken)
	{
		await _repository.Create(_mapper.Map<SalesPlan>(request.SalesPlan));
		await _repository.SaveChanges();
	}
}
