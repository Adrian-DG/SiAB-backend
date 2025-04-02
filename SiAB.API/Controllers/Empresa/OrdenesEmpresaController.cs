using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiAB.API.Filters;
using SiAB.API.Helpers;
using SiAB.Application.Contracts;
using SiAB.Core.DTO;
using SiAB.Core.DTO.Empresa;
using SiAB.Core.Entities.Empresa;
using SiAB.Core.Entities.Misc;
using SiAB.Core.Models;
using System.Linq.Expressions;

namespace SiAB.API.Controllers.Empresa
{
	[Route("api/ordenes-empresa")]
	public class OrdenesEmpresaController : GenericController<OrdenEmpresa>
	{
		public OrdenesEmpresaController(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService) : base(unitOfWork, mapper, userContextService)
		{
		}

		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetOrdenesEmpresa([FromRoute] int id, [FromQuery] PaginationFilter filters)
		{
			var result = await _uow.Repository<OrdenEmpresa>().GetListPaginateAsync(
					includes: new Expression<Func<OrdenEmpresa, object>>[] { oe => oe.Detalles },
					predicate: oe => oe.EmpresaId == id,
					selector: oe => new OrdenEmpresaModel
					{
						Id = oe.Id,
						Comentario = oe.Comentario,
						FechaEfectividad = oe.FechaEfectividad,
						Cantidad = oe.Detalles.Sum(d => d.Articulo.Cantidad),
					},
					page: filters.Page,
					pageSize: filters.Size,
					orderBy: oe => oe.OrderByDescending(o => o.FechaEfectividad)
				);

			return new JsonResult(result);
		}

		[HttpGet("{id:int}/documentos")]
		public async Task<IActionResult> GetOrdenEmpresaDocumentos([FromRoute] int id)
		{
			var result = await _uow.Repository<OrdenEmpresaDocumento>().GetListAsync(
				includes: new Expression<Func<OrdenEmpresaDocumento, object>>[] { oed => oed.TipoDocumento },
				predicate: oed => oed.OrdenEmpresaId == id,
				selector: oed => new
				{
					Id = oed.Id,
					Documento = oed.Archivo,
					TipoDocumento = oed.TipoDocumento.Nombre 
				}
			);

			return new JsonResult(result);
		}

		[HttpGet("{id:int}/articulos")]
		public async Task<IActionResult> GetOrdenEmpresaDetalles([FromRoute] int id)
		{
			var result = await _uow.Repository<OrdenEmpresaDetalle>().GetListAsync(
				includes: new Expression<Func<OrdenEmpresaDetalle, object>>[] { oed => oed.Articulo },
				predicate: oed => oed.OrdenEmpresaId == id,
				selector: oed => new
				{
					Id = oed.Id,
					Tipo = oed.Articulo.Tipo.Nombre,
					SubTipo = oed.Articulo.SubTipo.Nombre,
					Articulo = oed.Articulo.Serie,
					Cantidad = oed.Articulo.Cantidad,
				}
			);

			return new JsonResult(result);
		}

		[HttpPost("{id:int}")]
		[ServiceFilter(typeof(CreateAuditableFilter))]
		public async Task<IActionResult> CreateOrdenEmpresa([FromRoute] int id, [FromBody] CreateOrdenEmpresaDto createOrdenEmpresaDto)
		{
			await _uow.EmpresaRepository.CreateOrdenEmpresa(id, createOrdenEmpresaDto);
			return Ok();
		}

		
	}
}
