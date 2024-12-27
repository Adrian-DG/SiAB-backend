using Microsoft.AspNetCore.Mvc.Filters;
using SiAB.API.Controllers;
using SiAB.Core.Abstraction;

namespace SiAB.API.Filters
{
	public class CodUsuarioFilter : IAsyncActionFilter
	{
		private readonly GenericController _controller;

		public CodUsuarioFilter(GenericController controller)
		{
			_controller = controller;
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var codUsuario = context.HttpContext.User.FindFirst("CodUsuario")?.Value;

			if (codUsuario is null)
			{
				throw new System.Exception("No se encontró el código de usuario en el token.");
			}

			_controller._codUsuario = int.Parse(codUsuario);

			await next();
		}
		
	}
}
