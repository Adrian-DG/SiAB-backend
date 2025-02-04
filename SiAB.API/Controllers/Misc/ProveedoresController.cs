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
using SiAB.Core.Entities.Personal;

namespace SiAB.API.Controllers.Misc
{
	[Route("api/proveedores")]
	public class ProveedoresController : GenericController<Proveedor>
	{
		public ProveedoresController(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService) : base(unitOfWork, mapper, userContextService)
		{
		}

		[HttpPost]
		[ServiceFilter(typeof(NamedFilter<Proveedor>))]
		[ServiceFilter(typeof(CreateAuditableFilter))]
		public async Task<IActionResult> Create([FromBody] CreateProveedorDto createProveedorDto)
		{
			await _uow.ProveedorRepository.CreateProveedor_Licencia(createProveedorDto);
			return Ok();
		}

		[HttpGet]
		public async Task<IActionResult> Get([FromQuery] PaginationFilter filter)
		{
			var proveedores = await _uow.Repository<Proveedor>().GetListPaginateAsync(
					predicate: p => (int)p.CodInstitucion == _codInstitucionUsuario && p.Nombre.Contains(filter.SearchTerm ?? ""),
					selector: p => new
					{
						p.Id,
						p.Nombre,
						p.RNC,
						p.Telefono
					},
					orderBy: p => p.OrderBy(o => o.Nombre),
					page: filter.Page,
					pageSize: filter.Size
				);

			return new JsonResult(proveedores);
		}

		[HttpGet("{id:int}/licencias")]
		public async Task<IActionResult> GetById([FromRoute] int id)
		{
			var licencias = await _uow.Repository<LicenciaProveedor>().GetListAsync(
					predicate: p => p.ProveedorId == id,
					selector: p => new
					{
						p.Id,
						p.FechaEmision,
						p.FechaVencimiento,
						p.Numeracion,
						p.TipoLicencia,
						p.Archivo,
						p.DiasRestantes,
						p.StatusLicencia,
					},
					orderBy: p => p.OrderByDescending(o => o.FechaVencimiento)
				);

			return new JsonResult(licencias);
		}
	}
}
