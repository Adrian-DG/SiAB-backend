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
	[Route("api/tipos-documentos")]
	public class TiposDocumentosController : GenericController<TipoDocumento>
	{
		public TiposDocumentosController(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService) : base(unitOfWork, mapper, userContextService)
		{
		}

		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filters)
		{
			var result = await _uow.Repository<TipoDocumento>().GetListPaginateAsync(
				predicate: t => t.Nombre.Contains(filters.SearchTerm ?? ""),
				selector: t => new
				{
					Id = t.Id,
					Nombre = t.Nombre
				},
				orderBy: t => t.OrderBy(o => o.Nombre),
				page: filters.Page,
				pageSize: filters.Size
			);

			return new JsonResult(result);
		}

		[HttpGet("all")]
		public async Task<IActionResult> GetAllTiposDocumentos()
		{
			var result = await _uow.Repository<TipoDocumento>().GetListAsync(
				predicate: t => t.Nombre != null,
				selector: t => new NamedModel
				{
					Id = t.Id,
					Nombre = t.Nombre
				},
				orderBy: t => t.OrderBy(o => o.Nombre)
			);
			return new JsonResult(result);
		}

		[HttpPost]
		[ServiceFilter(typeof(NamedFilter<TipoDocumento>))]
		public async Task<IActionResult> Create([FromBody] CreateNamedEntityDto createNamedEntityDto)
		{
			await _uow.Repository<TipoDocumento>().AddAsync(new TipoDocumento { Nombre = createNamedEntityDto.Nombre });
			return Ok();
		}
	}
}
