﻿using Microsoft.CodeAnalysis.CSharp.Syntax;
using SiAB.API.Controllers;
using SiAB.API.Filters;
using SiAB.API.Helpers;
using SiAB.API.Middlewares;
using SiAB.Application.Contracts;
using SiAB.Core.Abstraction;
using SiAB.Core.Constants;
using SiAB.Infrastructure.Data;
using SiAB.Infrastructure.Interceptors;
using SiAB.Infrastructure.Repositories;
using SiAB.Infrastructure.Repositories.Belico;
using SiAB.Infrastructure.Repositories.Deposito;
using SiAB.Infrastructure.Repositories.Empresa;
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
				.AddScoped<IJCERepository, JCERepository>()
				.AddScoped<ISipffaaRepository, SipffaaRepository>()
				.AddScoped<IUserContextService, UserContextService>()
				.AddScoped<IUsuarioRepository, UsuarioRepository>()
				.AddScoped<IRoleRepository, RoleRepository>()
				.AddScoped<IReportRepository, ReportRepository>()
				.AddScoped<ISecuenciaRepository, SecuenciaRepository>()
				.AddScoped<ITransaccionRepository, TrasaccionRepository>()
				.AddScoped<IEmpresaRepository, EmpresaRepository>()
				.AddScoped<IDepositoRepository, DepositoRepository>()
				.AddScoped<CodUsuarioFilter>()
				.AddScoped<CodInstitucionFilter>()
				.AddScoped(typeof(NamedFilter<>))
				.AddScoped<INamedService, NamedService>()
				.AddScoped<IDatabaseConnectionService, DatabaseConnectionService>()
				.AddScoped<CreateAuditableFilter>()
				.AddScoped<UpdateAuditableFilter>()
				.AddHttpContextAccessor() // Register IHttpContextAccessor
				.AddScoped<CreateAuditableInterceptor>()
				.AddScoped<UpdateAuditableInterceptor>()				
				.AddSingleton<DapperContext>()
				.AddTransient<ApiResponseMiddleware>();
		}
	}
}
