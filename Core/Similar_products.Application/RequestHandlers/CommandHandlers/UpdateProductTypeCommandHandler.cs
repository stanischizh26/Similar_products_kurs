using MediatR;
using AutoMapper;
using Similar_products.Domain.Abstractions;
using Similar_products.Application.Requests.Commands;

namespace Similar_products.Application.RequestHandlers.CommandHandlers;

public class UpdateProductTypeCommandHandler : IRequestHandler<UpdateProductTypeCommand, bool>
{
	private readonly IProductTypeRepository _repository;
	private readonly IMapper _mapper;

	public UpdateProductTypeCommandHandler(IProductTypeRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<bool> Handle(UpdateProductTypeCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository.GetById(request.ProductType.Id, trackChanges: true);

        if (entity is null)
        {
            return false;
        }

		_mapper.Map(request.ProductType, entity);

		_repository.Update(entity);
		await _repository.SaveChanges();

		return true;
	}
}
