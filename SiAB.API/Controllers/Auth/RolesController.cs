using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiAB.API.Filters;
using SiAB.API.Helpers;
using SiAB.Application.Contracts;
using SiAB.Core.Entities.Auth;

namespace SiAB.API.Controllers.Auth
{
	[Authorize]
	[ApiController]
	[Route("api/permisos")]
	[TypeFilter(typeof(CodUsuarioFilter))]
	[TypeFilter(typeof(CodInstitucionFilter))]
	public class RolesController : ControllerBase
	{
		private readonly IUnitOfWork _uow;
		private readonly IUserContextService _userContextService;
		public int _codUsuario
		{
			get => _userContextService.CodUsuario;
			set => _userContextService.CodUsuario = value;
		}

		public int _codInstitucionUsuario
		{
			get => _userContextService.CodInstitucionUsuario;
			set => _userContextService.CodInstitucionUsuario = value;
		}
		public RolesController(IUnitOfWork unitOfWork, IUserContextService userContextService)
		{
			_uow = unitOfWork;
			_userContextService = userContextService;
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
