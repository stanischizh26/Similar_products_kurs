using Microsoft.EntityFrameworkCore;
using Similar_products.Domain.Entities;
using Similar_products.Domain.Abstractions;

namespace Similar_products.Infrastructure.Repositories;

public class SalesPlanRepository(AppDbContext dbContext) : ISalesPlanRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Create(SalesPlan entity) => await _dbContext.SalesPlans.AddAsync(entity);

    public async Task<IEnumerable<SalesPlan>> Get(bool trackChanges) =>
        await (!trackChanges 
            ? _dbContext.SalesPlans.Include(e => e.Enterprise).Include(e => e.Product).AsNoTracking() 
            : _dbContext.SalesPlans.Include(e => e.Enterprise).Include(e => e.Product)).ToListAsync();

    public async Task<SalesPlan?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.SalesPlans.Include(e => e.Enterprise).Include(e => e.Product).AsNoTracking() :
            _dbContext.SalesPlans.Include(e => e.Enterprise).Include(e => e.Product)).SingleOrDefaultAsync(e => e.Id == id);

    public void Delete(SalesPlan entity) => _dbContext.SalesPlans.Remove(entity);

    public void Update(SalesPlan entity) => _dbContext.SalesPlans.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
    public async Task<IEnumerable<SalesPlan>> GetPageAsync(int page, int pageSize, string? name)
    {
        var enterprises = await _dbContext.SalesPlans.ToListAsync();
        if (!string.IsNullOrWhiteSpace(name))
        {
            enterprises = enterprises.Where(p => p.Enterprise.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        return enterprises.Skip((page - 1) * pageSize)
            .Take(pageSize);

    }

    public async Task<int> CountAsync(string? name)
    {
        var enterpises = await _dbContext.SalesPlans.Include(e => e.Enterprise).Include(e => e.Product).ToListAsync();
        if (!string.IsNullOrWhiteSpace(name))
        {
            enterpises = enterpises.Where(p => p.Enterprise.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        return enterpises.Count;
    }
}

