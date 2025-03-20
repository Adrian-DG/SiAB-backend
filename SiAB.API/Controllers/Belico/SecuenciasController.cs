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

		private string GenerarSecuencia(int numero)
		{           
			var fecha_cadena = DateTime.Now.ToString("yyyy-MM-dd");
			var numero_cadena = "" + (numero < 10 ? "0" + numero : numero);

			return ((InstitucionEnum)_codInstitucionUsuario) switch
			{
				InstitucionEnum.MIDE => $"{numero_cadena}-{DateTime.Now.Year}",
				InstitucionEnum.ERD => throw new BaseException("No se ha definido la institución", HttpStatusCode.BadRequest),
				InstitucionEnum.ARD => throw new BaseException("No se ha definido la institución", HttpStatusCode.BadRequest),
				InstitucionEnum.FARD => throw new BaseException("No se ha definido la institución", HttpStatusCode.BadRequest),
				_ => throw new BaseException("No se ha definido la institución", HttpStatusCode.BadRequest)
			};
		}

		[HttpGet("generar")]
		public async Task<IActionResult> GetSecuenciaInstitucion()
		{
			var result = await _uow.Repository<Secuencia>().FindWhereAsync(
				predicate: s => s.CodInstitucion == (InstitucionEnum)_codInstitucionUsuario && s.FechaCreacion.Year == DateTime.Now.Year,
				selector: s => s.SecuenciaCadena);

			return new JsonResult(result);
		}

		[HttpPost]
		[ServiceFilter(typeof(CreateAuditableFilter))]
		public async Task<IActionResult> Create([FromBody] CreateSecuenciaDto createSecuenciaDto)
		{
			if (await _uow.Repository<Secuencia>().ConfirmExistsAsync(s => s.CodInstitucion == (InstitucionEnum)_codInstitucionUsuario && s.FechaCreacion.Year ==  DateTime.Now.Year))
			{
				throw new BaseException("Ya existe una secuencia para esta institución", HttpStatusCode.BadRequest);
			}

			var secuencia = String.IsNullOrEmpty(createSecuenciaDto.Secuencia) 
				? throw new BaseException("La secuencia no puede ser nula o vacía", HttpStatusCode.BadRequest)
				: new Secuencia { SecuenciaCadena = GenerarSecuencia(int.Parse(createSecuenciaDto.Secuencia)), SecuenciaNumero = int.Parse(createSecuenciaDto.Secuencia), CodInstitucion = (InstitucionEnum)_codInstitucionUsuario, UsuarioId = _codUsuario };			

			await _uow.Repository<Secuencia>().AddAsync(secuencia);
			return Ok();
		}
	}
}
