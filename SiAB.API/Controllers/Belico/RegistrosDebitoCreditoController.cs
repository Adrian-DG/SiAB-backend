﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiAB.Application.Contracts;
using SiAB.Core.DTO.CargoDescargo;
using SiAB.Core.Entities.Belico;

namespace SiAB.API.Controllers.Belico
{
	[Route("api/registros")]
	[ApiController]
	public class RegistrosDebitoCreditoController : GenericController<RegistroDebitoCredito>
	{
		public RegistrosDebitoCreditoController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
		{
		}

		[HttpPost("create")]
		public async Task<IActionResult> Create([FromBody] CreateRDC_Dto cargoDescargoDto)
		{
			var registroCargoDescargo = _mapper.Map<RegistroDebitoCredito>(cargoDescargoDto);
			await _uow.Repository<RegistroDebitoCredito>().AddAsync(registroCargoDescargo);
			return Ok();
		}

		[HttpGet("debito")]
		public async Task<IActionResult> GetArticulosDebito([FromQuery] string debitadoA)
		{
			var articulos = await _uow.RDCRepository.GetArticulosDebito(debitadoA);
			return new JsonResult(articulos);
		}
	}
}
