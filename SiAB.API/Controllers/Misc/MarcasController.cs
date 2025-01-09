using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    [ApiController]
	[Route("api/marcas")]
	public class MarcasController : GenericController<Marca>
	{
		public MarcasController(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService) : base(unitOfWork, mapper, userContextService)
		{
		}

		[HttpGet]
		public async Task<IActionResult> Get([FromQuery] PaginationFilter filter)
		{
			var result = await _uow.Repository<Marca>().GetListPaginateAsync<MarcaDetaiModell>(
				predicate: m => m.Nombre.Contains(filter.SearchTerm ?? ""),
				selector: m => new MarcaDetaiModell
				{
					Id = m.Id,
					Nombre = m.Nombre
				},
				orderBy: m => m.OrderBy(o => o.Nombre),
				page: filter.Page,
				pageSize: filter.Size
			);

			return new JsonResult(result);
		}

		[HttpGet("filtrar")]
		public async Task<IActionResult> GetFilterMarcas([FromQuery] string nombre)
		{
			var result = await _uow.Repository<Marca>().GetListAsync<NamedModel>(
				predicate: m => m.Nombre.Contains(nombre ?? ""),
				selector: m => new MarcaDetaiModell
				{
					Id = m.Id,
					Nombre = m.Nombre
				},
				orderBy: m => m.OrderBy(o => o.Nombre)
			);

			return new JsonResult(result);
		}

		[HttpPost]
		[ServiceFilter(typeof(NamedFilter<Marca>))]
		public async Task<IActionResult> Create([FromBody] CreateNamedEntityDto createNamedEntityDto)
		{
			var marca = new Marca { Nombre = createNamedEntityDto.Nombre };

			await _uow.Repository<Marca>().AddAsync(marca);

			return Ok();
		}

		[HttpPut("{id:int}")]
		[ServiceFilter(typeof(NamedFilter<Marca>))]
		public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateNamedEntityDto createNamedEntityDto)
		{
			var entity = await _uow.Repository<Marca>().GetByIdAsync(id);
			if (entity is null) return NotFound();
			entity.Nombre = createNamedEntityDto.Nombre;
			await _uow.Repository<Marca>().Update(entity);
			return Ok();
		}

	}
}
