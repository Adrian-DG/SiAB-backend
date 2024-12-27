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
    [Route("api/modelos")]
	public class ModelosController : GenericController
	{
		public ModelosController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
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
					Nombre = m.Nombre,
					Marca = m.Marca.Nombre
				},
				orderBy: m => m.OrderBy(o => o.Nombre),
				page: filter.Page,
				pageSize: filter.Size
			);

			return new JsonResult(result);
		}
	}
}
