﻿using Microsoft.CodeAnalysis.CSharp.Syntax;
using SiAB.API.Controllers;
using SiAB.API.Filters;
using SiAB.API.Helpers;
using SiAB.API.Middlewares;
using SiAB.Application.Contracts;
using SiAB.Core.Abstraction;
using SiAB.Infrastructure.Data;
using SiAB.Infrastructure.Repositories;
using SiAB.Infrastructure.Repositories.JCE;

namespace SiAB.API.Services
{
	public static class ApplicationServices
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			return services
				.AddScoped<IUnitOfWork, UnitOfWork>()
				.AddScoped(typeof(IRepository<>), typeof(Repository<>))
				.AddScoped<IRDCRepository, RDCRepository>()
				.AddScoped<IJCERepository, JCERepository>()
				.AddScoped<ISipffaaRepository, SipffaaRepository>()
				.AddScoped<CodUsuarioFilter>()
				.AddScoped<CodInstitucionFilter>()
				.AddScoped<IUserContextService, UserContextService>()
				.AddScoped<INamedService, NamedService>()
				.AddScoped(typeof(NamedFilter<>))
				.AddSingleton<DapperContext>()
				.AddTransient<ApiResponseMiddleware>(); // Register the middleware here
		}
	}
}
