using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SiAB.API.Filters;
using SiAB.API.Helpers;
using SiAB.Application.Contracts;
using SiAB.Core.DTO;
using SiAB.Core.DTO.Inventario;
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

		[HttpPut("{id:int}")]
		[ServiceFilter(typeof(UpdateAuditableFilter))]
		public async Task<IActionResult> UpdateArticulo([FromRoute] int id, [FromBody] UpdateArticuloDto updateArticuloDto)
		{
			var articulo = await _uow.Repository<Articulo>().GetByIdAsync(id);

			if (articulo == null) return NotFound();

			if (articulo.CategoriaId > 0) articulo.CategoriaId = updateArticuloDto.CategoriaId;

			if (articulo.TipoId > 0) articulo.TipoId = updateArticuloDto.TipoId;

			if (articulo.SubTipoId > 0) articulo.SubTipoId = updateArticuloDto.SubTipoId;

			if (articulo.MarcaId > 0) articulo.MarcaId = updateArticuloDto.MarcaId;

			if (articulo.ModeloId > 0) articulo.ModeloId = updateArticuloDto.ModeloId;

			if (!string.IsNullOrEmpty(updateArticuloDto.Serie) && articulo.Serie != updateArticuloDto.Serie) articulo.Serie = updateArticuloDto.Serie;

			await _uow.Repository<Articulo>().Update(articulo);

			return Ok();
		}
	}
}
