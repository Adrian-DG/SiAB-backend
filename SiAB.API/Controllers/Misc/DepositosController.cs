using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiAB.Application.Contracts;
using SiAB.Core.Entities.Misc;
using SiAB.Core.Models;

namespace SiAB.API.Controllers.Misc
{
	[Route("api/depositos")]
	[ApiController]
	public class DepositosController : GenericController<Deposito>
	{
		public DepositosController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
		{
		}

		[HttpGet("filtrar")]
		public async Task<IActionResult> GetDepositos([FromQuery] string nombre)
		{		
			var depositos = await _uow.Repository<Deposito>().GetListAsync(
				predicate: d => d.Nombre.Contains(nombre ?? string.Empty), 
				selector: d => d
			);

			var formatedDepositos = _mapper.Map<IEnumerable<NamedModel>>(depositos);

			return new JsonResult(formatedDepositos);
		}
	}
}
