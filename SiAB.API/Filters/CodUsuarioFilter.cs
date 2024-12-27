using Microsoft.AspNetCore.Mvc.Filters;
using SiAB.API.Controllers;
using SiAB.Core.Abstraction;

namespace SiAB.API.Filters
{
    public class CodUsuarioFilter : IActionFilter
	{
		private readonly GenericController _controller;

		public CodUsuarioFilter(GenericController controller)
		{
			_controller = controller;
		}

		public void OnActionExecuted(ActionExecutedContext context)
		{
			var codUsuario = context.HttpContext.User.FindFirst("CodUsuario")?.Value;

			if (codUsuario is null)
			{
				throw new System.Exception("No se encontró el código de usuario en el token.");
			}

			_controller._codUsuario = int.Parse(codUsuario);
		}

		public void OnActionExecuting(ActionExecutingContext context)
		{
			var codUsuario = context.HttpContext.User.FindFirst("CodUsuario")?.Value;

			if (codUsuario is null)
			{
				throw new System.Exception("No se encontró el código de usuario en el token.");
			}

			_controller._codUsuario = int.Parse(codUsuario);
		}
	}
}
