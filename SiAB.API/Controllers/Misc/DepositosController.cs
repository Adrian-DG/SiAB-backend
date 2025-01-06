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
using SiAB.API.Helpers;
using SiAB.Core.Exceptions;
using System.Net;
using SiAB.API.Extensions;
using SiAB.API.Filters;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SiAB.API.Controllers.Misc
{
    [Route("api/depositos")]
	[ApiController]
	public class DepositosController : GenericController
	{
		public DepositosController(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService) : base(unitOfWork, mapper, userContextService)
		{
		}

		[HttpGet]
		public async Task<IActionResult> Get([FromQuery] PaginationFilter filter)
		{
			var depositos = await _uow.Repository<Deposito>().GetListPaginateAsync(
				includes: new Expression<Func<Deposito, object>>[] { m => m.Dependencia },
				predicate: d => d.Nombre.Contains(filter.SearchTerm ?? "") && d.Dependencia.Institucion == (InstitucionEnum)_codInstitucionUsuario,
				selector: d => new 
				{ 
					Id = d.Id,
					Nombre = d.Nombre,
					EsFuncion = d.EsFuncion,
					Dependencia = d.Dependencia.Nombre
				},
				orderBy: d => d.OrderBy(o => o.Nombre),
				page: filter.Page,
				pageSize: filter.Size
			);

			return new JsonResult(depositos);
		}

		[HttpGet("filtrar")]
		public async Task<IActionResult> GetDepositos([FromQuery] string nombre)
		{		
			var depositos = await _uow.Repository<Deposito>().GetListAsync(
				includes: new Expression<Func<Deposito, object>>[] { m => m.Dependencia },
				predicate: d => d.Nombre.Contains(nombre ?? string.Empty) && d.Dependencia.Institucion.Equals(_codInstitucionUsuario), 				
				selector: d => new NamedModel { Id = d.Id, Nombre = d.Nombre },
				orderBy: d => d.OrderBy(o => o.Nombre)
			);

			return new JsonResult(depositos);
		}

		[HttpPost]
		[ServiceFilter(typeof(NamedFilter<Deposito>))]
		public async Task<IActionResult> Create([FromBody] CreateDepositoDto createDepositoDto)
		{
			var deposito = new Deposito
			{
				Nombre = createDepositoDto.Nombre,
				DependenciaId = createDepositoDto.DependenciaId,
				EsFuncion = createDepositoDto.EsFuncion
			};

			await _uow.Repository<Deposito>().AddAsync(deposito);

			return Ok();
		}

	}
}
