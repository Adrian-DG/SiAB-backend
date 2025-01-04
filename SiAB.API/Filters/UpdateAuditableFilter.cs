using Microsoft.AspNetCore.Mvc.Filters;
using SiAB.API.Controllers;
using SiAB.Core.Constants;
using SiAB.Core.Exceptions;
using SiAB.Infrastructure.Interceptors;

namespace SiAB.API.Filters
{
	public class UpdateAuditableFilter : IAsyncActionFilter
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly UpdateAuditableInterceptor _updateAuditableInterceptor;

		public UpdateAuditableFilter(IHttpContextAccessor httpContextAccessor, UpdateAuditableInterceptor updateAuditableInterceptor)
		{
			_httpContextAccessor = httpContextAccessor;
			_updateAuditableInterceptor = updateAuditableInterceptor;
		}
		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var isAuthenticated = _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

			if (!isAuthenticated)
			{
				throw new BaseException("No se encontró un usuario autenticado.", System.Net.HttpStatusCode.Unauthorized);
			}

			var codUsuario = _httpContextAccessor?.HttpContext?.User.FindFirst(TokenPropertiesContants.CodUsuario)?.Value;			

			if (codUsuario is null)
			{
				throw new BaseException("No se encontró el código de usuario o de institución en el token.", System.Net.HttpStatusCode.BadRequest);
			}

			if (isAuthenticated && context.Controller is GenericController controller && context.HttpContext.Request.Method == HttpMethods.Put)
			{
				_updateAuditableInterceptor.SetParameters(int.Parse(codUsuario));
			}

			await next();
		}
	}
}
