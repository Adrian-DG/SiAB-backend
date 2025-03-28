using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OfficeOpenXml;
using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SiAB.API.Attributes;
using SiAB.API.Filters;
using SiAB.API.Helpers;
using SiAB.Application.Contracts;
using SiAB.Core.DTO.Transacciones;
using SiAB.Core.Entities.Belico;
using SiAB.Core.Entities.Misc;
using SiAB.Core.Enums;
using SiAB.Core.Exceptions;
using SiAB.Infrastructure.Data;
using System.Data;
using System.Linq.Expressions;
using System.Net;

namespace SiAB.API.Controllers.Belico
{
	[ApiController]
	[AvoidCustomResponse]
	[Route("api/transacciones")]
	public class TransaccionesController : GenericController<Transaccion>
	{
		private readonly IDatabaseConnectionService _connectionService;

		public TransaccionesController(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService) : base(unitOfWork, mapper, userContextService)
		{
			QuestPDF.Settings.License = LicenseType.Community;
		}

		[HttpGet("filter-transacciones-serie")]
		public async Task<IActionResult> GetTransaccionesBySerie([FromQuery] string serie)
		{
			var result = await _uow.TransaccionRepository.GetTransaccionesBySerie(serie);
			return new JsonResult(result);
		}


		[HttpGet("filter-articulos-origen-transaccion")]
		public async Task<IActionResult> GetArticulosOrigenTransaccion([FromQuery] TipoTransaccionEnum tipoOrigen, [FromQuery] string origen)
		{
			var result = await _uow.TransaccionRepository.GetArticulosOrigenTransaccion(tipoOrigen, origen);
			return new JsonResult(result);
		}

		[HttpGet("{id:int}/documentos-transaccion")]
		public async Task<IActionResult> GetDocumentosTransaccion([FromRoute] int id)
		{
			var result = await _uow.Repository<DocumentoTransaccion>().GetListAsync(
				includes: new Expression<System.Func<DocumentoTransaccion, object>>[] { dt => dt.TipoDocumento },
				predicate: dt => dt.TransaccionId == id,
				selector: dt => new
				{
					Id = dt.Id,
					NumeracionDocumento = dt.NumeracionDocumento,
					Archivo = dt.Archivo,
					TipoDocumento = dt.TipoDocumento.Nombre
				}
			);

			return new JsonResult(result);
		}

		

		
		[HttpPost("registrar-cargo-descargo")]
		[ServiceFilter(typeof(CreateAuditableFilter))]
		public async Task<IActionResult> CreateTransaccionCargoDescargo([FromBody] CreateTransaccionCargoDescargoDto transaccionCargoDescargoDto)
		{
			var transaccion = await _uow.TransaccionRepository.CreateTransaccionCargoDescargo(transaccionCargoDescargoDto);

			return Ok(transaccion);
		}

		[HttpPost("adjuntar-formulario-53")]
		[ServiceFilter(typeof(CreateAuditableFilter))]
		public async Task<IActionResult> AdjuntarFormulario53([FromBody] AdjuntarFormularioTransaccionDto adjuntarFormulario53Dto)
		{
			await _uow.TransaccionRepository.SaveFormulario53(adjuntarFormulario53Dto.Id, adjuntarFormulario53Dto.Url);
			return Ok();
		}

		[HttpPost("generar-formulario-53")]
		public IActionResult GenerateFormulario53([FromBody] InputTransaccionReport53 InputTransaccionReport53)
		{
			var formulario53 = _uow.TransaccionRepository.GenerateFormulario53(InputTransaccionReport53);
			return File(formulario53, "application/pdf", "Formulario53.pdf");
		}

		[HttpPost("upload-excel-relacion-articulos")]
		[ServiceFilter(typeof(CreateAuditableFilter))]
		public async Task<IActionResult> UploadRelacionArticulos(IFormFile File, [FromQuery] InputOrigenDestinoDto inputOrigenDestinoDto)
		{
			await _uow.TransaccionRepository.UploadRelacionArticulos(File, inputOrigenDestinoDto);
			return Ok();
		}


	}
}

