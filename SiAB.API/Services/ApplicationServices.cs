using SiAB.Application.Contracts;
using SiAB.Infrastructure.Data;
using SiAB.Infrastructure.Repositories;

namespace SiAB.API.Services
{
	public static class ApplicationServices
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			return services
				.AddScoped<IUnitOfWork, UnitOfWork>()
				.AddScoped(typeof(IRepository<>), typeof(Repository<>))
				.AddSingleton<JwtService>();
		}
	}
}
