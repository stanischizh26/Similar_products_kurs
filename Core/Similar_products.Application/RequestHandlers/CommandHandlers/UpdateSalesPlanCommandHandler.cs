using MediatR;
using AutoMapper;
using Similar_products.Domain.Abstractions;
using Similar_products.Application.Requests.Commands;

namespace Similar_products.Application.RequestHandlers.CommandHandlers;

public class UpdateSalesPlanCommandHandler : IRequestHandler<UpdateSalesPlanCommand, bool>
{
	private readonly ISalesPlanRepository _repository;
	private readonly IMapper _mapper;

	public UpdateSalesPlanCommandHandler(ISalesPlanRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<bool> Handle(UpdateSalesPlanCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository.GetById(request.SalesPlan.Id, trackChanges: true);

        if (entity is null)
        {
            return false;
        }

		_mapper.Map(request.SalesPlan, entity);

		_repository.Update(entity);
		await _repository.SaveChanges();

		return true;
	}
}
