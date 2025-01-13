using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiAB.API.Filters;
using SiAB.API.Helpers;
using SiAB.Application.Contracts;
using SiAB.Core.DTO;
using SiAB.Core.DTO.Misc;
using SiAB.Core.Entities.Misc;
using SiAB.Core.Entities.Personal;
using SiAB.Core.Enums;
using SiAB.Core.Models;

namespace SiAB.API.Controllers.Misc
{
	[Route("api/dependencias")]
	[ApiController]
	public class DependenciasController : GenericController<Dependencia>
	{
		public DependenciasController(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService) : base(unitOfWork, mapper, userContextService)
		{
		}

		[HttpGet]
		public async Task<IActionResult> GetDependenciasPaginated([FromQuery] PaginationFilter filter)
		{
			var result = await _uow.Repository<Dependencia>().GetListPaginateAsync(
					predicate: d => d.Nombre.Contains(filter.SearchTerm ?? ""),
					selector: d => new { Id = d.Id, Nombre = d.Nombre, Institucion = d.Institucion.ToString(), Externa = d.EsExterna },
					orderBy: d => d.OrderBy(o => o.Nombre)
				);

			return new JsonResult(result);
		}

		[HttpGet("filtrar")]
		public async Task<IActionResult> GetDependencias([FromQuery] string nombre)
		{
			var result = await _uow.Repository<Dependencia>().GetListAsync(
					predicate: d => d.Nombre.Contains(nombre ?? ""),
					selector: d => new NamedModel { Id = d.Id, Nombre = d.Nombre },
					orderBy: d => d.OrderBy(o => o.Nombre)
				);

			return new JsonResult(result);
		}

		[HttpPost]
		[ServiceFilter(typeof(NamedFilter<Dependencia>))]
		public async Task<IActionResult> Create([FromBody] CreateDependenciaDto createDependenciaDto)
		{
			var dependencia = new Dependencia
			{
				Nombre = createDependenciaDto.Nombre,
				EsExterna = createDependenciaDto.EsExterna,
				Institucion = (InstitucionEnum)_codInstitucionUsuario
			};

			await _uow.Repository<Dependencia>().AddAsync(dependencia);
			return Ok();
		}

		[HttpPut("{id:int}")]
		public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateNamedEntityDto updateNamedEntityDto)
		{
			var entity = await _uow.Repository<Dependencia>().GetByIdAsync(id);
			if (entity is null) return NotFound();
			entity.Nombre = updateNamedEntityDto.Nombre;
			await _uow.Repository<Dependencia>().Update(entity);
			return Ok();
		}

	}
}
