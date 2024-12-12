using Similar_products.Domain.Entities;

namespace Similar_products.Domain.Abstractions;

public interface ISalesPlanRepository 
{
	Task<IEnumerable<SalesPlan>> Get(bool trackChanges);
	Task<SalesPlan?> GetById(Guid id, bool trackChanges);
    Task Create(SalesPlan entity);
    void Delete(SalesPlan entity);
    void Update(SalesPlan entity);
    Task SaveChanges();
    Task<IEnumerable<SalesPlan>> GetPageAsync(int page, int pageSize, string? name);
    Task<int> CountAsync(string? name);
}

