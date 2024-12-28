using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiAB.API.Extensions;
using SiAB.API.Filters;
using SiAB.API.Helpers;
using SiAB.Application.Contracts;
using SiAB.Core.DTO;
using SiAB.Core.DTO.Misc;
using SiAB.Core.Entities.Misc;
using SiAB.Core.Exceptions;
using SiAB.Core.Models;
using System.Net;

namespace SiAB.API.Controllers.Misc
{
    [Route("api/propiedades")]
	[ApiController]
	public class PropiedadesController : GenericController
	{
		public PropiedadesController(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService) : base(unitOfWork, mapper, userContextService)
		{
		}

		[HttpGet]
		public async Task<IActionResult> Get([FromQuery] PaginationFilter filter)
		{
			var propiedades = await _uow.Repository<Propiedad>().GetListPaginateAsync(
					predicate: p => p.Nombre.Contains(filter.SearchTerm ?? ""),
					selector: p => new NamedModel
					{
						Id = p.Id,
						Nombre = p.Nombre
					},
					page: filter.Page,
					pageSize: filter.Size
				);

			return new JsonResult(propiedades);
		}

		[HttpPost]
		[ServiceFilter(typeof(NamedFilter<Propiedad>))]
		public async Task<IActionResult> Create([FromBody] CreateNamedEntityDto createNamedEntityDto)
		{    
			await _uow.Repository<Propiedad>().AddAsync(new Propiedad { Nombre = createNamedEntityDto.Nombre });

			return Ok();
		}
	}
}
