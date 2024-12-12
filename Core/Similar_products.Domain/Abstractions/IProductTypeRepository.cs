using Similar_products.Domain.Entities;

namespace Similar_products.Domain.Abstractions;

public interface IProductTypeRepository 
{
	Task<IEnumerable<ProductType>> Get(bool trackChanges);
	Task<ProductType?> GetById(Guid id, bool trackChanges);
    Task Create(ProductType entity);
    void Delete(ProductType entity);
    void Update(ProductType entity);
    Task SaveChanges();
    Task<IEnumerable<ProductType>> GetPageAsync(int page, int pageSize, string? name);
    Task<int> CountAsync(string? name);
}

