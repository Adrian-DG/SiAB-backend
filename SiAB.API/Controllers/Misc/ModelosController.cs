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
    [Route("api/modelos")]
	public class ModelosController : GenericController<Modelo>
	{
		public ModelosController(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService) : base(unitOfWork, mapper, userContextService)
		{
		}

		[HttpGet]
		public async Task<IActionResult> Get([FromQuery] PaginationFilter filter)
		{
			var result = await _uow.Repository<Modelo>().GetListPaginateAsync(
				predicate: m => m.Nombre.Contains(filter.SearchTerm ?? "") || m.Marca.Nombre.Contains(filter.SearchTerm ?? ""),
				includes: new Expression<Func<Modelo, object>>[] { m => m.Marca },
				selector: m => new 
				{
					Id = m.Id,
					Foto = m.Foto,
					Nombre = m.Nombre,
					MarcaId = m.MarcaId,
					Marca = m.Marca.Nombre
				},
				orderBy: m => m.OrderBy(o => o.Nombre),
				page: filter.Page,
				pageSize: filter.Size
			);

			return new JsonResult(result);
		}

		[HttpGet("filtrar-por-marca")]
		public async Task<IActionResult> GetModelosByMarca([FromQuery] int marca)
		{
			var result = await _uow.Repository<Modelo>().GetListAsync(
				predicate: m => m.MarcaId == marca,
				selector: m => new NamedModel
				{
					Id = m.Id,
					Nombre = m.Nombre
				},
				orderBy: m => m.OrderBy(o => o.Nombre)
			);
			return new JsonResult(result);
		}


		[HttpPost]
		[ServiceFilter(typeof(NamedFilter<Modelo>))]
		public async Task<IActionResult> Create([FromBody] CreateModeloDto createModeloDto)
		{
			var modelo = new Modelo
			{
				Foto = createModeloDto.Foto,
				Nombre = createModeloDto.Nombre,
				MarcaId = createModeloDto.MarcaId
			};

			await _uow.Repository<Modelo>().AddAsync(modelo);
			return Ok();
		}

		[HttpPut("{id:int}")]
		public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateModeloDto updateModeloDto)
		{
			var entity = await _uow.Repository<Modelo>().GetByIdAsync(id);
			if (entity is null) return NotFound();
			entity.Foto = updateModeloDto.Foto;
			entity.Nombre = updateModeloDto.Nombre;
			entity.MarcaId = updateModeloDto.MarcaId;
			await _uow.Repository<Modelo>().Update(entity);
			return Ok();
		}
	}
}
