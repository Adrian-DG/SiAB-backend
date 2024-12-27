using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiAB.API.Helpers;
using SiAB.Application.Contracts;
using SiAB.Core.DTO;
using SiAB.Core.Entities.Misc;
using SiAB.Core.Models;

namespace SiAB.API.Controllers.Misc
{
    [ApiController]
	[Route("api/marcas")]
	public class MarcasController : GenericController
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
		
	}
}
