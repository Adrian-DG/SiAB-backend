using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiAB.Application.Contracts;
using SiAB.Core.DTO;
using SiAB.Core.Entities.Misc;
using SiAB.Core.Entities.Personal;
using System.Linq.Expressions;

namespace SiAB.API.Controllers.Misc
{
	[Route("api/funciones")]
	[ApiController]
	public class FuncionesController : GenericController<Funcion>
	{
		public FuncionesController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
		{
		}

		[HttpGet]
		public async Task<IActionResult> GetFuncionesPaginated([FromQuery] PaginationFilter filter)
		{
			var result = await _uow.Repository<Funcion>().GetListPaginateAsync(
					predicate: f => f.Nombre.Contains(filter.SearchTerm ?? ""),
					includes: new Expression<Func<Funcion, object>>[] { t => t.Dependencia },
					selector: f => new 
					{ 
						Id = f.Id, 
						Nombre = f.Nombre, 
						Dependencia = f.Dependencia.Nombre 
					},
					page: filter.Page,
					pageSize: filter.Size
				);

			return new JsonResult(result);
		}
	}
}
