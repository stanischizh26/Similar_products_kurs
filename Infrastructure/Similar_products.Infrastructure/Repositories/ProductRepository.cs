using Microsoft.EntityFrameworkCore;
using Similar_products.Domain.Entities;
using Similar_products.Domain.Abstractions;

namespace Similar_products.Infrastructure.Repositories;

public class ProductRepository(AppDbContext dbContext) : IProductRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Create(Product entity) => await _dbContext.Products.AddAsync(entity);

    public async Task<IEnumerable<Product>> Get(bool trackChanges) =>
        await (!trackChanges 
            ? _dbContext.Products.Include(e => e.ProductType).AsNoTracking() 
            : _dbContext.Products.Include(e => e.ProductType)).ToListAsync();

    public async Task<Product?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.Products.Include(e => e.ProductType).AsNoTracking() :
            _dbContext.Products.Include(e => e.ProductType)).SingleOrDefaultAsync(e => e.Id == id);

    public void Delete(Product entity) => _dbContext.Products.Remove(entity);

    public void Update(Product entity) => _dbContext.Products.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();

    public async Task<IEnumerable<Product>> GetPageAsync(int page, int pageSize, string? name)
    {
        var products = await _dbContext.Products.Include(e => e.ProductType).ToListAsync();
        if(!string.IsNullOrWhiteSpace(name))
        {
            products = products.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        return products.Skip((page - 1) * pageSize)
            .Take(pageSize);
    }

    public async Task<int> CountAsync(string? name)
    {
        var products = await _dbContext.Products.Include(e => e.ProductType).ToListAsync();
        if (!string.IsNullOrWhiteSpace(name))
        {
            products = products.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        return products.Count;
    }
}

