using Microsoft.AspNetCore.Mvc.Filters;
using SiAB.API.Helpers;
using SiAB.Core.Constants;

namespace SiAB.API.Filters
{
	public class PermisoFilter : IAsyncActionFilter
	{
		private readonly IUserContextService _userContextService;

		public PermisoFilter(IUserContextService userContextService)
		{
			_userContextService = userContextService;
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			string? rolesString = context.HttpContext.User.FindFirst(TokenPropertiesContants.Roles)?.Value;

			if (string.IsNullOrEmpty(rolesString)) throw new System.Exception("No se encontró el código de usuario en el token.");

			string[] roles = rolesString.Split(',');

			if (!roles.Any()) throw new System.Exception("No se encontró el código de usuario en el token.");

			_userContextService.Roles = roles;

			await next();
		}
	}
}
