using MediatR;
using Similar_products.Domain.Abstractions;
using Similar_products.Application.Requests.Commands;

namespace Similar_products.Application.RequestHandlers.CommandHandlers;

public class DeleteProductCommandHandler(IProductRepository repository) : IRequestHandler<DeleteProductCommand, bool>
{
	private readonly IProductRepository _repository = repository;

	public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository.GetById(request.Id, trackChanges: false);

        if (entity is null)
        {
            return false;
        }

        _repository.Delete(entity);
        await _repository.SaveChanges();

        return true;
	}
}
