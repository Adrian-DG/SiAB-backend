using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiAB.API.Helpers;
using SiAB.Application.Contracts;
using SiAB.Core.DTO;
using SiAB.Core.DTO.Misc;
using SiAB.Core.Entities.Personal;

namespace SiAB.API.Controllers.Misc
{
	[Route("api/[controller]")]
	[ApiController]
	public class DependenciasController : GenericController
	{
		public DependenciasController(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService) : base(unitOfWork, mapper, userContextService)
		{
		}

		[HttpGet]
		public async Task<IActionResult> GetDependenciasPaginated([FromQuery] PaginationFilter filter)
		{
			var result = await _uow.Repository<Dependencia>().GetListPaginateAsync(
					predicate: d => d.Nombre.Contains(filter.SearchTerm ?? ""),
					includes: null,
					selector: d => new { Id = d.Id, Nombre = d.Nombre, Institucion = d.Institucion.ToString(), Externa = d.EsExterna },
					orderBy: d => d.OrderBy(o => o.Nombre)
				);

			return new JsonResult(result);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateDependenciaDto createDependenciaDto)
		{
			
		}
	}
}
