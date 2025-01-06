using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiAB.API.Helpers;
using SiAB.Application.Contracts;
using SiAB.Core.DTO;
using SiAB.Core.Entities.Belico;
using SiAB.Core.Models.miscelaneos;
using System.Linq;
using System.Linq.Expressions;

namespace SiAB.API.Controllers.Belico
{
	[Route("api/series")]
	[ApiController]
	public class SeriesController : GenericController
	{
		public SeriesController(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService) : base(unitOfWork, mapper, userContextService)
		{
		}

		[HttpGet]
		public async Task<IActionResult> GetSeriesPaginated([FromQuery] PaginationFilter filter)
		{
			var result = await _uow.Repository<Serie>().GetListPaginateAsync(
					predicate: s => s.SerieCode.Contains(filter.SearchTerm ?? ""),
					includes: new Expression<Func<Serie, object>>[] { s => s.Articulo, s => s.Propiedad },
					selector: s => new SerieDetailModel
					{
						Id = s.Id,
						Serie = s.SerieCode,
						Articulo = s.Articulo.Nombre,
						Propiedad = s.Propiedad.Nombre,
						Comentario = s.Comentario,
					},
					page: filter.Page,
					pageSize: filter.Size
				);

			return new JsonResult(result);
		}
	}
}
