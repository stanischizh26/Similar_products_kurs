using MediatR;
using AutoMapper;
using Similar_products.Domain.Entities;
using Similar_products.Domain.Abstractions;
using Similar_products.Application.Requests.Commands;

namespace Similar_products.Application.RequestHandlers.CommandHandlers;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
{
	private readonly IProductRepository _repository;
	private readonly IMapper _mapper;

	public CreateProductCommandHandler(IProductRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
	{
		await _repository.Create(_mapper.Map<Product>(request.Product));
		await _repository.SaveChanges();
	}
}
