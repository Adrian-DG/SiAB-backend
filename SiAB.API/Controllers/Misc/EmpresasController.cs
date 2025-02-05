using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiAB.API.Filters;
using SiAB.API.Helpers;
using SiAB.Application.Contracts;
using SiAB.Core.DTO;
using SiAB.Core.DTO.Misc;
using SiAB.Core.Entities.Inventario;
using SiAB.Core.Entities.Misc;
using System.Linq.Expressions;


namespace SiAB.API.Controllers.Misc
{
	[Route("api/empresas")]
	public class EmpresasController : GenericController<Empresa>
	{
		public EmpresasController(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService) : base(unitOfWork, mapper, userContextService)
		{
		}

		[HttpPost]
		[ServiceFilter(typeof(NamedFilter<Empresa>))]
		[ServiceFilter(typeof(CreateAuditableFilter))]
		public async Task<IActionResult> Create([FromBody] CreateEmpresaDto createEmpresaDto)
		{
			await _uow.EmpresaRepository.CreateEmpresa_Licencia(createEmpresaDto);
			return Ok();
		}

		[HttpGet]
		public async Task<IActionResult> Get([FromQuery] PaginationFilter filter)
		{
			var empresas = await _uow.Repository<Empresa>().GetListPaginateAsync(
					predicate: p => (int)p.CodInstitucion == _codInstitucionUsuario && p.Nombre.Contains(filter.SearchTerm ?? ""),
					selector: p => new
					{
						p.Id,
						p.Nombre,
						p.RNC,
						p.Titular,
						p.Telefono
					},
					orderBy: p => p.OrderBy(o => o.Nombre),
					page: filter.Page,
					pageSize: filter.Size
				);

			return new JsonResult(empresas);
		}

		[HttpGet("{id:int}/licencias")]
		public async Task<IActionResult> GetById([FromRoute] int id)
		{
			var licencias = await _uow.Repository<LicenciaEmpresa>().GetListAsync(
					includes: new Expression<Func<LicenciaEmpresa, object>>[] { l => l.TipoDocumento },
					predicate: l => l.EmpresaId == id,
					selector: l => new
					{
						Id = l.Id,
						FechaEmision = l.FechaEmision,
						FechaVigencia = l.FechaVigencia,
						FechaVencimiento = l.FechaVencimiento,
						Numeracion = l.Numeracion,
						documento = l.TipoDocumento.Nombre,
						Archivo = l.Archivo,
						DiasDeVigencia = l.DiasDeVigencia,
						DiasRestantes = l.DiasRestantes
					},
					orderBy: l => l.OrderByDescending(o => o.FechaVencimiento)
				);

			return new JsonResult(licencias);
		}

		[HttpGet("tipos-licencias")]
		public async Task<IActionResult> GetTipoDocumentosLicencias()
		{
			var tipos = await _uow.Repository<TipoDocumento>().GetListAsync(
					predicate: t => t.Id >= 3 && t.Id <= 5,
					selector: t => new
					{
						Id = t.Id,
						Nombre = t.Nombre,
					},
					orderBy: t => t.OrderBy(o => o.Nombre)
				);

			return new JsonResult(tipos);
		}
	}
}
