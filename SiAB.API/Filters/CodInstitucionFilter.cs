using Microsoft.AspNetCore.Mvc.Filters;
using SiAB.API.Controllers;
using SiAB.Core.Abstraction;

namespace SiAB.API.Filters
{
	public class CodInstitucionFilter : IActionFilter
	{
		private readonly GenericController _controller;

		public CodInstitucionFilter(GenericController controller)
		{
			_controller = controller;	
		}

		public GenericController Controller => _controller;

		public void OnActionExecuted(ActionExecutedContext context)
		{
			var codInstitucion = context.HttpContext.User.FindFirst("CodInstitucion")?.Value;

			if (codInstitucion is null)
			{
				throw new System.Exception("No se encontró el código de institución en el token.");
			}

			_controller._codInstitucionUsuario = int.Parse(codInstitucion);
		}

		public void OnActionExecuting(ActionExecutingContext context)
		{
			var codInstitucion = context.HttpContext.User.FindFirst("CodInstitucion")?.Value;

			if (codInstitucion is null)
			{
				throw new System.Exception("No se encontró el código de institución en el token.");
			}

			_controller._codInstitucionUsuario = int.Parse(codInstitucion);
		}
	}
}
