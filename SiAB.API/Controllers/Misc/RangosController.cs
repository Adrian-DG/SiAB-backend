using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiAB.API.Helpers;
using SiAB.Application.Contracts;
using SiAB.Core.Entities.Personal;

namespace SiAB.API.Controllers.Misc
{
	[Route("api/rangos")]
	public class RangosController : GenericController
	{
		public RangosController(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService) : base(unitOfWork, mapper, userContextService)
		{
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var rangos = await _uow.Repository<Rango>().GetAllAsync();
			return new JsonResult(rangos.OrderBy(r => r.Id));
		}
	}
}
