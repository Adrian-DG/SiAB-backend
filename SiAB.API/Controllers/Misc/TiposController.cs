using System.Linq.Expressions;
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
	[Route("api/tipos")]
	[ApiController]
	public class TiposController : GenericController<Tipo>
	{
		public TiposController(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService) : base(unitOfWork, mapper, userContextService)
		{
		}

		[HttpGet]
		public async Task<IActionResult> Get([FromQuery] PaginationFilter filter)
		{
			var result = await _uow.Repository<Tipo>().GetListPaginateAsync<object>(
				predicate: t => t.Nombre.Contains(filter.SearchTerm ?? ""),
				includes: new Expression<Func<Tipo, object>>[] { t => t.Categoria },
				selector: t => new 
				{
					Id = t.Id,
					Nombre = t.Nombre,
					CategoriaId = t.CategoriaId,
					Categoria = t.Categoria.Nombre
				},
				page: filter.Page,
				pageSize: filter.Size
			);

			return new JsonResult(result);
		}

		[HttpGet("filtrar")]
		public async Task<IActionResult> GetAll()
		{
			var result = await _uow.Repository<Tipo>().GetAllAsync();

			var formatedResult = result.Select(c => new NamedModel
			{
				Id = c.Id,
				Nombre = c.Nombre
			}).OrderBy(c => c.Nombre);

			return new JsonResult(formatedResult);
		}

		[HttpGet("filtrar-por-categoria")]
		public async Task<IActionResult> GetAll([FromQuery] int categoria)
		{
			var result = await _uow.Repository<Tipo>().GetListAsync(
				predicate: t => t.CategoriaId == categoria,
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
		[ServiceFilter(typeof(NamedFilter<Tipo>))]
		public async Task<IActionResult> Create([FromBody] CreateTipoDto createTipoDto)
		{
			var tipo = new Tipo { Nombre = createTipoDto.Nombre, CategoriaId = createTipoDto.CategoriaId };
			await _uow.Repository<Tipo>().AddAsync(tipo);
			return Ok();
		}

		[HttpPut("{id:int}")]
		public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CreateTipoDto createTipoDto)
		{
			var tipo = await _uow.Repository<Tipo>().GetByIdAsync(id);
			
			if (tipo is null)
			{
				return NotFound();
			}

			tipo.Nombre = createTipoDto.Nombre;
			tipo.CategoriaId = createTipoDto.CategoriaId;
			await _uow.Repository<Tipo>().Update(tipo);
			return Ok();
		}

	}
}
