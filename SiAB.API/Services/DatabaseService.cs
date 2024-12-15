using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SiAB.Core.Entities.Auth;
using SiAB.Infrastructure.Data;

namespace SiAB.API.Services
{
	public static class DatabaseService
	{
		public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
		{		
			
			services.AddDbContext<AppDbContext>(opt =>
			{
				var connectionString = environment.IsProduction()
				? configuration.GetConnectionString("ProdConnection")
				: configuration.GetConnectionString("DevConnection");
				
				opt.UseSqlServer(connectionString, builder => builder.MigrationsAssembly("SiAB.API"));
			});

			services.AddIdentity<Usuario, Role>(opt => {
				opt.SignIn.RequireConfirmedAccount = false;
				opt.Password.RequireNonAlphanumeric = false;
				opt.Password.RequireUppercase = false;
			}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

			return services;
		}
	}
}
