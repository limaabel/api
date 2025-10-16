using EngineApplication.Domain.Interfaces;
using InfrastructureApplication.Persistence.Context;
using InfrastructureApplication.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InfrastructureApplication.Persistence;

public static class DependencyInjection
{
	public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<CrmDbContext>(options =>
			options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

		services.AddScoped<ICustomerRepository, CustomerRepository>();
		services.AddScoped<IOrderRepository, OrderRepository>();
		services.AddScoped<IProductRepository, ProductRepository>();
		services.AddScoped<ISyncLogRepository, SyncLogRepository>();

		return services;
	}
}