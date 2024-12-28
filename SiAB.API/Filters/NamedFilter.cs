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
			var jsonSerializerOptions = new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
				AllowTrailingCommas = true
			};

			var requestJson = context.ActionArguments.TryGetValue(key: "createNamedEntityDto", out var value) 
				? JsonSerializer.Serialize(value, jsonSerializerOptions) 
				: throw new BaseException("No se pudo serializar el objeto", HttpStatusCode.BadRequest);

			var requestDto = JsonSerializer.Deserialize<CreateNamedEntityDto>(requestJson, jsonSerializerOptions);

			if (requestDto != null && await _namedService.ExistByNameAsync<T>(requestDto.Nombre))
			{
				throw new BaseException($"Ya existe una registro con ese nombre", HttpStatusCode.BadRequest);
			}

			await next();
		}
	}
}
