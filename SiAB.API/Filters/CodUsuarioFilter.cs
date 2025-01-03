using Microsoft.AspNetCore.Mvc.Filters;
using SiAB.API.Controllers;
using SiAB.API.Helpers;
using SiAB.Core.Abstraction;
using SiAB.Core.Constants;

namespace SiAB.API.Filters
{
	public class CodUsuarioFilter : IAsyncActionFilter
	{
		private readonly IUserContextService _userContextService;

		public CodUsuarioFilter(IUserContextService userContextService)
		{
			_userContextService = userContextService;
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var codUsuario = context.HttpContext.User.FindFirst(TokenPropertiesContants.CodUsuario)?.Value;

			if (codUsuario is null)
			{
				throw new System.Exception("No se encontró el código de usuario en el token.");
			}

			_userContextService.CodUsuario = int.Parse(codUsuario);

			await next();
		}
		
	}
}
