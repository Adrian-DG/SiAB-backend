using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SiAB.API.Helpers;
using SiAB.Application.Contracts;
using SiAB.Core.DTO;
using SiAB.Core.Entities.Inventario;
using SiAB.Core.Entities.Misc;
using SiAB.Core.Enums;
using System.Linq.Expressions;

namespace SiAB.API.Controllers.Inventario
{
	[Route("api/articulos")]
	[ApiController]
	public class ArticulosController : GenericController<Articulo>
	{
		public ArticulosController(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService) : base(unitOfWork, mapper, userContextService)
		{
		}

		[HttpGet]
		public async Task<IActionResult> GetArticulos([FromQuery] PaginationFilter filter)
		{
			var result = await _uow.Repository<Articulo>().GetListPaginateAsync(
					includes: new Expression<Func<Articulo, object>>[] { 
						m => m.Marca, 
						m => m.Modelo,
						m => m.Categoria,
						m => m.Calibre,
						m => m.SubTipo,
						m => m.Tipo
					},
					predicate: a => a.Serie.Contains(filter.SearchTerm ?? "") && a.CodInstitucion == (InstitucionEnum)_codInstitucionUsuario,
					selector: a => new
					{
						Id = a.Id,
						Categoria = a.Categoria.Nombre,
						Tipo = a.Tipo.Nombre,
						SubTipo = a.SubTipo.Nombre,
						Marca = a.Marca.Nombre,
						Modelo = a.Modelo.Nombre,
						Serie = a.Serie,
					}
				);

			return new JsonResult(result);
		}
	}
}
