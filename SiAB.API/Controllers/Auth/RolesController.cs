using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiAB.API.Helpers;
using SiAB.Application.Contracts;

namespace SiAB.API.Controllers.Auth
{
	[Route("api/permisos")]
	public class RolesController : GenericController
	{
		public RolesController(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService) : base(unitOfWork, mapper, userContextService)
		{
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var roles = await _uow.RoleRepository.GetListAsync(
				predicate: x => x.Id > 0,
				selector: x => new
				{
					x.Id,
					x.Name,
					x.Descripcion
				},
				orderBy: x => x.OrderBy(x => x.Name)
			);

			return new JsonResult(roles);
		}
	}
}
