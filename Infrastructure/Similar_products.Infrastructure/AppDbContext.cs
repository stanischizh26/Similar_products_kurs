using Microsoft.EntityFrameworkCore;
using Similar_products.Domain.Entities;

namespace Similar_products.Infrastructure;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
	public DbSet<Product> Products { get; set; }
	public DbSet<Enterprise> Enterprises { get; set; }
	public DbSet<ProductionPlan> ProductionPlans { get; set; }
	public DbSet<ProductType> ProductTypes { get; set; }
	public DbSet<SalesPlan> SalesPlans { get; set; }

    public DbSet<User> Users { get; set; }
}

