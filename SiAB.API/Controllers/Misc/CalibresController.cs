using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiAB.API.Filters;
using SiAB.API.Helpers;
using SiAB.Application.Contracts;
using SiAB.Core.DTO;
using SiAB.Core.DTO.Misc;
using SiAB.Core.Entities.Misc;
using SiAB.Core.Models;

namespace SiAB.API.Controllers.Misc
{
	[Route("api/calibres")]
	[ApiController]
	public class CalibresController : GenericController<Calibre>
	{
		public CalibresController(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService) : base(unitOfWork, mapper, userContextService)
		{
		}

		[HttpGet]
		public async Task<IActionResult> Get([FromQuery] PaginationFilter filter)
		{
			var calibres = await _uow.Repository<Calibre>().GetListPaginateAsync(
					predicate: c => c.Nombre.Contains(filter.SearchTerm ?? ""),
					selector: c => new NamedModel { Id = c.Id, Nombre = c.Nombre },
					orderBy: c => c.OrderBy(o => o.Nombre),
					page: filter.Page,
					pageSize: filter.Size
				);

			return new JsonResult(calibres);
		}

		[HttpPost]
		[ServiceFilter(typeof(NamedFilter<Calibre>))]
		public async Task<IActionResult> Create([FromBody] CreateNamedEntityDto createNamedEntityDto)
		{
			await _uow.Repository<Calibre>().AddAsync(new Calibre { Nombre = createNamedEntityDto.Nombre });
			return Ok();
		}

		[HttpPut("{id:int}")]
		[ServiceFilter(typeof(NamedFilter<Calibre>))]
		public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateNamedEntityDto updateNamedEntityDto)
		{
			var entity = await _uow.Repository<Calibre>().GetByIdAsync(id);
			if (entity is null) return NotFound();
			entity.Nombre = updateNamedEntityDto.Nombre;
			await _uow.Repository<Calibre>().Update(entity);
			return Ok();
		}

	}
}
