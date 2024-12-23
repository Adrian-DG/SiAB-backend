using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiAB.Application.Contracts;
using SiAB.Core.DTO;
using SiAB.Core.DTO.Misc;
using SiAB.Core.Entities.Belico;
using SiAB.Core.Models;

namespace SiAB.API.Controllers.Belico
{
	[Route("api/calibres")]
	[ApiController]
	public class CalibresController : GenericController<Calibre>
	{
		public CalibresController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
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
		public async Task<IActionResult> Create([FromBody] CreateNamedEntityDto entity)
		{
			await _uow.Repository<Calibre>().AddAsync(new Calibre { Nombre = entity.Nombre });
			return Ok();
		}
	}
}
