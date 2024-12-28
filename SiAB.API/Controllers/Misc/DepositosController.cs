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

namespace SiAB.API.Controllers.Misc
{
    [Route("api/depositos")]
	[ApiController]
	public class DepositosController : GenericController
	{
		public DepositosController(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService) : base(unitOfWork, mapper, userContextService)
		{
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
		
	}
}
