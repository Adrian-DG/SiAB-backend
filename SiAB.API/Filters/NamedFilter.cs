using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NuGet.Protocol;
using SiAB.API.Helpers;
using SiAB.Core.Abstraction;
using SiAB.Core.DTO.Misc;
using SiAB.Core.Exceptions;
using System.Net;
using System.Text.Json;

namespace SiAB.API.Filters
{
	public class NamedFilter<T> : IAsyncActionFilter where T : NamedEntityMetadata
	{
		private readonly INamedService _namedService;
		public NamedFilter(INamedService namedService)
		{
			_namedService = namedService;
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			foreach (var argument in context.ActionArguments.Values)
			{
				if (argument is CreateNamedEntityDto requestDto)
				{
					if (await _namedService.ExistByNameAsync<T>(requestDto.Nombre.Trim()))
					{
						throw new BaseException("Ya existe un registro con ese nombre", HttpStatusCode.BadRequest);
					}
				}
			}			

			await next();
		}
	}
}
