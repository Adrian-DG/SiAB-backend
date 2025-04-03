using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiAB.API.Filters;
using SiAB.API.Helpers;
using SiAB.Application.Contracts;
using SiAB.Core.DTO.Secuencias;
using SiAB.Core.Entities.Belico;
using SiAB.Core.Enums;
using SiAB.Core.Exceptions;
using System.Net;

namespace SiAB.API.Controllers.Belico
{
	[Route("api/secuencias")]
	public class SecuenciasController : GenericController<Secuencia>
	{
		public SecuenciasController(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService) : base(unitOfWork, mapper, userContextService)
		{
		}

		[HttpGet("generar")]
		public async Task<IActionResult> GetSecuenciaInstitucion()
		{
			var result = await _uow.SecuenciaRepository.GetSecuenciaInstitucion(_codInstitucionUsuario);
			return new JsonResult(result);
		}

		[HttpPost]
		[ServiceFilter(typeof(CreateAuditableFilter))]
		public async Task<IActionResult> Create()
		{
			await _uow.SecuenciaRepository.CreateSecuencia(_codInstitucionUsuario);
			return Ok();
		}
	}
}
