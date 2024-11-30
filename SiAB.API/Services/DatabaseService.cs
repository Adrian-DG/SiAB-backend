using Microsoft.EntityFrameworkCore;
using SiAB.Infrastructure.Data;

namespace SiAB.API.Services
{
	public static class DatabaseService
	{
		public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
		{
			var connectionString = environment.IsProduction()
				? configuration.GetConnectionString("ProdConnection")
				: configuration.GetConnectionString("DevConnection");

			return services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connectionString, builder => builder.MigrationsAssembly("SiAB.API")));
		}
	}
}
