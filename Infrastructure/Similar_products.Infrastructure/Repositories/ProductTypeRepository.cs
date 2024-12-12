using Microsoft.EntityFrameworkCore;
using Similar_products.Domain.Entities;
using Similar_products.Domain.Abstractions;

namespace Similar_products.Infrastructure.Repositories;

public class ProductTypeRepository(AppDbContext dbContext) : IProductTypeRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Create(ProductType entity) => await _dbContext.ProductTypes.AddAsync(entity);

    public async Task<IEnumerable<ProductType>> Get(bool trackChanges) =>
        await (!trackChanges 
            ? _dbContext.ProductTypes.AsNoTracking() 
            : _dbContext.ProductTypes).ToListAsync();

    public async Task<ProductType?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.ProductTypes.AsNoTracking() :
            _dbContext.ProductTypes).SingleOrDefaultAsync(e => e.Id == id);

    public void Delete(ProductType entity) => _dbContext.ProductTypes.Remove(entity);

    public void Update(ProductType entity) => _dbContext.ProductTypes.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
    public async Task<IEnumerable<ProductType>> GetPageAsync(int page, int pageSize, string? name)
    {
        var items = await _dbContext.ProductTypes.ToListAsync();
        if (!string.IsNullOrWhiteSpace(name))
        {
            items = items.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        return items.Skip((page - 1) * pageSize)
            .Take(pageSize);
    }

    public async Task<int> CountAsync(string? name)
    {
        var items = await _dbContext.ProductTypes.ToListAsync();
        if (!string.IsNullOrWhiteSpace(name))
        {
            items = items.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        return items.Count;
    }
}

