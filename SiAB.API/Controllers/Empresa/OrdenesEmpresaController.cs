﻿using AutoMapper;
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
using SiAB.Core.Models.Empresa;
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
					includes: new Expression<Func<OrdenEmpresa, object>>[] { oe => oe.Articulos },
					predicate: oe => oe.EmpresaId == id,
					selector: oe => new OrdenEmpresaModel
					{
						Id = oe.Id,
						Comentario = oe.Comentario,
						FechaEfectividad = oe.FechaEfectividad,
						CantidadRecibida = oe.Articulos != null ? oe.Articulos.Sum(a => a.CantidadRecibida) : 0,
						CantidadEntregada = oe.Articulos != null ? oe.Articulos.Sum(a => a.CantidadEntregada) : 0,
						Documentos = oe.Documentos != null ? oe.Documentos.Count() : 0
					},
					page: filters.Page,
					pageSize: filters.Size,
					orderBy: oe => oe.OrderByDescending(o => o.FechaEfectividad)
				);

			return new JsonResult(result);
		}

		[HttpGet("{id:int}/detalles")]
		public async Task<IActionResult> GetDetalleOrdenEmpresa([FromRoute] int id)
		{
			var result = await _uow.EmpresaRepository.GetDetalleOrdenEmpresa(id);
			return new JsonResult(result);
		}

		[HttpPost("{id:int}")]
		[ServiceFilter(typeof(CreateAuditableFilter))]
		public async Task<IActionResult> CreateOrdenEmpresa([FromRoute] int id, [FromBody] CreateOrdenEmpresaDto createOrdenEmpresaDto)
		{
			await _uow.EmpresaRepository.CreateOrdenEmpresa(id, createOrdenEmpresaDto);
			return Ok();
		}

		[HttpPost("{id:int}/adjuntar-documento")]
		[ServiceFilter(typeof(UpdateAuditableFilter))]
		public async Task<IActionResult> UpdateOrdenAdjuntarDocumento([FromRoute] int id, [FromBody] CreateOrdenEmpresaDocumentoDto ordenEmpresaDocumentoDto)
		{
			await _uow.EmpresaRepository.UpdateOrdenAdjuntarDocumento(id, ordenEmpresaDocumentoDto);
			return Ok();
		}

		[HttpPut("{id:int}/articulos-entregados")]
		[ServiceFilter(typeof(UpdateAuditableFilter))]
		public async Task<IActionResult> UpdateOrdenArticulosEntregados([FromRoute] int id, [FromBody] List<OrdenEmpresaArticuloModel> ordenEmpresaArticulos)
		{
			await _uow.EmpresaRepository.UpdateOrdenArticulosEntregados(id, ordenEmpresaArticulos);
			return Ok();
		}


	}
}
