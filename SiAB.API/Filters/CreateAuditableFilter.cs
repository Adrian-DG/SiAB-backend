using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Query.Internal;
using SiAB.API.Controllers;
using SiAB.Core.Abstraction;
using SiAB.Core.Constants;
using SiAB.Core.Exceptions;
using SiAB.Infrastructure.Interceptors;

namespace SiAB.API.Filters
{
	public class CreateAuditableFilter : IAsyncActionFilter
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly CreateAuditableInterceptor _createAuditableInterceptor;

		public CreateAuditableFilter(IHttpContextAccessor httpContextAccessor, CreateAuditableInterceptor createAuditableInterceptor)
		{
			_httpContextAccessor = httpContextAccessor;
			_createAuditableInterceptor = createAuditableInterceptor;
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var isAuthenticated = _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;			

			if (!isAuthenticated)
			{
				throw new BaseException("No se encontró un usuario autenticado.", System.Net.HttpStatusCode.Unauthorized);
			}

			var codUsuario = _httpContextAccessor?.HttpContext?.User.FindFirst(TokenPropertiesContants.CodUsuario)?.Value;
			var codInstitucion = _httpContextAccessor?.HttpContext?.User.FindFirst(TokenPropertiesContants.CodInstitucion)?.Value;

			if (codUsuario is null || codInstitucion is null)
			{
				throw new BaseException("No se encontró el código de usuario o de institución en el token.", System.Net.HttpStatusCode.BadRequest);
			}

			if (isAuthenticated && context.Controller is GenericController<EntityMetadata> controller && context.HttpContext.Request.Method == HttpMethods.Post)
			{
				_createAuditableInterceptor.SetParameters(int.Parse(codUsuario), int.Parse(codInstitucion));
			}

			await next();
		}
	}
}
