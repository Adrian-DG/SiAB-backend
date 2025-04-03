using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiAB.API.Helpers;
using SiAB.Application.Contracts;
using SiAB.Core.DTO;
using SiAB.Core.DTO.Empresa;
using SiAB.Core.Entities.Empresa;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace SiAB.API.Controllers.Empresa
{
	[Route("api/empresas")]
	public class EmpresasController : GenericController<SiAB.Core.Entities.Empresa.Empresa>
	{
		public EmpresasController(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService) : base(unitOfWork, mapper, userContextService)
		{
		}

		[HttpGet]
		public async Task<IActionResult> GetEmpresas([FromQuery] PaginationFilter filter)
		{
			var result = await _uow.Repository<SiAB.Core.Entities.Empresa.Empresa>().GetListPaginateAsync(
					predicate: e => e.Nombre.Contains(filter.SearchTerm ?? ""),
					selector: e => new
					{
						Id = e.Id,
						Nombre = e.Nombre,
						RNC = e.RNC,
						Telefonos = e.Contactos.Select(c => c.Telefono),
						Titulares = e.Titulares.Select(t => new { t.Identificacion, Nombre = String.Concat(t.Nombre, " ", t.Apellido) })
					},
					page: filter.Page,
					pageSize: filter.Size
				);

			return new JsonResult(result);
		}

		[HttpPost]
		public async Task<IActionResult> CreateEmpresa([FromBody] CreateEmpresaDto createEmpresaDto)
		{
			await _uow.EmpresaRepository.CreateEmpresa(createEmpresaDto);
			return Ok();
		}

	}
}
