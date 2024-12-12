using Similar_products.Domain.Entities;

namespace Similar_products.Domain.Abstractions;

public interface IProductRepository 
{
	Task<IEnumerable<Product>> Get(bool trackChanges);
	Task<Product?> GetById(Guid id, bool trackChanges);
    Task Create(Product entity);
    void Delete(Product entity);
    void Update(Product entity);
    Task SaveChanges();
    Task<IEnumerable<Product>> GetPageAsync(int page, int pageSize, string? name);
    Task<int> CountAsync(string? name);
}

