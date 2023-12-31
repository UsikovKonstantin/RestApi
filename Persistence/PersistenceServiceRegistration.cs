using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DatabaseContext;

namespace Persistence;

public static class PersistenceServiceRegistration
{
	public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<ProductDatabaseContext>(options =>
		{
			options.UseSqlServer(configuration.GetConnectionString("ProductDatabaseConnectionString"));
		});

		return services;
	}
}
