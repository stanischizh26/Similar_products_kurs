using Similar_products.Domain.Entities;

namespace Similar_products.Domain.Abstractions;

public interface IProductionPlanRepository 
{
	Task<IEnumerable<ProductionPlan>> Get(bool trackChanges);
	Task<ProductionPlan?> GetById(Guid id, bool trackChanges);
    Task Create(ProductionPlan entity);
    void Delete(ProductionPlan entity);
    void Update(ProductionPlan entity);
    Task SaveChanges();
    Task<IEnumerable<ProductionPlan>> GetPageAsync(int page, int pageSize, string? name);
    Task<int> CountAsync(string? name);
}

