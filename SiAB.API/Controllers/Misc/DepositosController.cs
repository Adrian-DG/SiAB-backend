using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiAB.Application.Contracts;
using SiAB.Core.DTO;
using SiAB.Core.Entities.Misc;
using SiAB.Core.Models;
using SiAB.Core.Models.miscelaneos;
using System.Linq.Expressions;
using SiAB.API.Attributes;
using SiAB.Core.Enums;
using SiAB.Core.DTO.Misc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SiAB.API.Controllers.Misc
{
	[Route("api/depositos")]
	[ApiController]
	public class DepositosController : GenericController<Deposito>
	{
		public DepositosController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
		{
		}

		[HttpGet]
		public async Task<IActionResult> GetDepositosPaginated([FromQuery] PaginationFilter filter)
		{
			var includeProps = new Expression<Func<Deposito, object>>[] { d => d.Funcion, d => d.Funcion.Dependencia };

			var depositos = await _uow.Repository<Deposito>().GetListPaginateAsync(
				predicate: d => d.Nombre.Contains(filter.SearchTerm ?? string.Empty),
				includes: includeProps,
				selector: d => new DepositoDetailModel
				{
					Id = d.Id,
					Nombre = d.Nombre,
					Funcion = d.Funcion.Nombre ?? "Sin Función",
					Dependencia = d.Funcion.Dependencia.Nombre ?? "Sin Dependencia"
				},
				page: filter.Page,
				pageSize: filter.Size
			);

			return new JsonResult(depositos);
		}

		[HttpGet("filtrar")]
		public async Task<IActionResult> GetDepositos([FromQuery] string nombre)
		{		
			var depositos = await _uow.Repository<Deposito>().GetListAsync(
				predicate: d => d.Nombre.Contains(nombre ?? string.Empty), 
				includes: null,
				selector: d => new NamedModel { Id = d.Id, Nombre = d.Nombre },
				orderBy: d => d.OrderBy(o => o.Nombre)
			);

			return new JsonResult(depositos);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateDepositoDto createDepositoDto)
		{
			await _uow.Repository<Deposito>().AddAsync(new Deposito { Nombre = createDepositoDto.Nombre, FuncionId = createDepositoDto.FuncionId });
			return Ok();
		}

		
	}
}
