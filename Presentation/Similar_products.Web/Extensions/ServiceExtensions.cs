using Microsoft.EntityFrameworkCore;
using Similar_products.Infrastructure;
using Similar_products.Infrastructure.Repositories;
using Similar_products.Domain.Abstractions;

namespace Similar_products.Web.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        });

    public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("DbConnection"), b =>
                b.MigrationsAssembly("Similar_products.Infrastructure")));
    }

    public static void ConfigureServices(this IServiceCollection services)
    {
		services.AddScoped<IProductRepository, ProductRepository>();
		services.AddScoped<IEnterpriseRepository, EnterpriseRepository>();
		services.AddScoped<IProductionPlanRepository, ProductionPlanRepository>();
		services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
		services.AddScoped<ISalesPlanRepository, SalesPlanRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}
