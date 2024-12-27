using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SiAB.API.Controllers;
using SiAB.Core.Abstraction;

namespace SiAB.API.Filters
{
	public class CodInstitucionFilter : IAsyncActionFilter
	{
		private readonly GenericController _controller;

		public CodInstitucionFilter(GenericController controller)
		{
			_controller = controller;	
		}
		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var codInstitucion = context.HttpContext.User.FindFirst("CodInstitucion")?.Value;

			if (codInstitucion is null)
			{
				throw new System.Exception("No se encontró el código de institución en el token.");
			}

			_controller._codInstitucionUsuario = int.Parse(codInstitucion);

			await next();
		}
	}
}
