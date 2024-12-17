using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiAB.Application.Contracts;
using SiAB.Core.DTO;
using SiAB.Core.Entities.Misc;
using SiAB.Core.Models;

namespace SiAB.API.Controllers.Misc
{
	[Route("api/tipos")]
	[ApiController]
	public class TiposController : GenericController<Tipo>
	{
		public TiposController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
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
					Categoria = t.Categoria.Nombre
				},
				page: filter.Page,
				pageSize: filter.Size
			);

			return new JsonResult(result);
		}
		
	}
}
