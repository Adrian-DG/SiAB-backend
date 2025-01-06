using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SiAB.API.Controllers;
using SiAB.API.Helpers;
using SiAB.Core.Abstraction;
using SiAB.Core.Constants;

namespace SiAB.API.Filters
{
	public class CodInstitucionFilter : IAsyncActionFilter
	{
		private readonly IUserContextService _userContextService;

		public CodInstitucionFilter(IUserContextService userContextService)
		{
			_userContextService = userContextService;
		}
		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var codInstitucion = context.HttpContext.User.FindFirst(TokenPropertiesContants.CodInstitucion)?.Value;

			if (codInstitucion is null)
			{
				throw new System.Exception("No se encontró el código de institución en el token.");
			}

			_userContextService.CodInstitucionUsuario = int.Parse(codInstitucion);

			await next();
		}
	}
}
