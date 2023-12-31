using Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DatabaseContext;
using Persistence.Repositories;

namespace Persistence;

public static class PersistenceServiceRegistration
{
	public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<ProductDatabaseContext>(options =>
		{
			options.UseSqlServer(configuration.GetConnectionString("ProductDatabaseConnectionString"));
		});

		services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
		services.AddScoped<ICategoryRepository, CategoryRepository>();
		services.AddScoped<IProductRepository, ProductRepository>();

		return services;
	}
}
