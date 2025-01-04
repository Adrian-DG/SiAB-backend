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
using System.Linq.Expressions;

namespace SiAB.API.Controllers.Misc
{
	[Route("api/subtipos")]
	[ApiController]
	public class SubTiposController : GenericController
	{
		public SubTiposController(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService) : base(unitOfWork, mapper, userContextService)
		{
		}

		[HttpGet]
		public async Task<IActionResult> Get([FromQuery] PaginationFilter filter)
		{
			var subtipos = await _uow.Repository<SubTipo>().GetListPaginateAsync(
					includes: new Expression<Func<SubTipo, object>>[] { st => st.Tipo },
					predicate: st => st.Nombre.Contains(filter.SearchTerm ?? ""),
					selector: st => new
					{
						Id = st.Id,
						Nombre = st.Nombre,
						Tipo = st.Tipo.Nombre
					},
					page: filter.Page,
					pageSize: filter.Size
				);

			return new JsonResult(subtipos);
		}

		[HttpPost]
		[ServiceFilter(typeof(NamedFilter<SubTipo>))]
		public async Task<IActionResult> Create([FromBody] CreateSubTipoDto createSubTipoDto)
		{
			var subTipo = new SubTipo { Nombre = createSubTipoDto.Nombre, TipoId = createSubTipoDto.TipoId };
			await _uow.Repository<SubTipo>().AddAsync(subTipo);
			return Ok();
		}
	}
}
