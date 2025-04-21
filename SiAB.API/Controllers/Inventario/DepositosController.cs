using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SiAB.Application.Contracts;
using SiAB.Core.DTO;
using SiAB.Core.Models;
using SiAB.Core.Models.miscelaneos;
using System.Linq.Expressions;
using SiAB.API.Attributes;
using SiAB.Core.Enums;
using SiAB.Core.DTO.Misc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SiAB.API.Helpers;
using SiAB.API.Filters;
using SiAB.Core.Entities.Inventario;

namespace SiAB.API.Controllers.Inventario
{
	[Route("api/depositos")]
	[ApiController]
	public class DepositosController : GenericController<Deposito>
	{
		public DepositosController(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService) : base(unitOfWork, mapper, userContextService)
		{
		}

		[HttpGet]
		public async Task<IActionResult> Get([FromQuery] PaginationFilter filter)
		{
			var depositos = await _uow.Repository<Deposito>().GetListPaginateAsync(
				includes: new Expression<Func<Deposito, object>>[] { m => m.Dependencia },
				predicate: d => d.Nombre.Contains(filter.SearchTerm ?? "") && d.DependenciaId == (int)_codInstitucionUsuario,
				selector: d => new
				{
					d.Id,
					d.Nombre,
					d.EsFuncion,
					d.DependenciaId,
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
				includes: null,
				predicate: d => d.Nombre.Contains(nombre.ToUpper() ?? string.Empty) && d.DependenciaId.Equals(_codInstitucionUsuario),
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

		[HttpPut("{id:int}")]
		[ServiceFilter(typeof(NamedFilter<Deposito>))]
		public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateDepositoDto updateDepositoDto)
		{
			var entity = await _uow.Repository<Deposito>().GetByIdAsync(id);
			if (entity is null) return NotFound();
			entity.Nombre = updateDepositoDto.Nombre;
			entity.DependenciaId = updateDepositoDto.DependenciaId;
			await _uow.Repository<Deposito>().Update(entity);
			return Ok();
		}

		[HttpGet("stock")]
		[AuthorizeRole(UsuarioRolesEnum.ADMINISTRADOR_GENERAL, UsuarioRolesEnum.INVENTARIO_BELICO_VISUALIZAR)]
		public async Task<IActionResult> GetStockByInventario()
		{
			var result = await _uow.DepositoRepository.GetStockByInventario();
			return new JsonResult(result);
		}

	}
}
