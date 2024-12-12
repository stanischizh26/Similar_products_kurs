using MediatR;
using AutoMapper;
using Similar_products.Domain.Entities;
using Similar_products.Domain.Abstractions;
using Similar_products.Application.Requests.Commands;

namespace Similar_products.Application.RequestHandlers.CommandHandlers;

public class CreateProductTypeCommandHandler : IRequestHandler<CreateProductTypeCommand>
{
	private readonly IProductTypeRepository _repository;
	private readonly IMapper _mapper;

	public CreateProductTypeCommandHandler(IProductTypeRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task Handle(CreateProductTypeCommand request, CancellationToken cancellationToken)
	{
		await _repository.Create(_mapper.Map<ProductType>(request.ProductType));
		await _repository.SaveChanges();
	}
}
