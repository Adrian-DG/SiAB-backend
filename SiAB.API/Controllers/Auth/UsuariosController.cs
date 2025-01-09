using System.Linq.Expressions;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiAB.API.Filters;
using SiAB.API.Helpers;
using SiAB.Application.Contracts;
using SiAB.Core.DTO;
using SiAB.Core.DTO.Auth;
using SiAB.Core.Entities.Auth;
using SiAB.Core.Enums;
using SiAB.Core.Exceptions;
using SiAB.Core.Models.Auth;

namespace SiAB.API.Controllers.Auth
{
	[Authorize]
	[ApiController]
	[Route("api/usuarios")]
	[TypeFilter(typeof(CodUsuarioFilter))]
	[TypeFilter(typeof(CodInstitucionFilter))]
	public class UsuariosController : ControllerBase
	{
		private readonly UserManager<Usuario> _userManager;
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

		public UsuariosController(UserManager<Usuario> userManager, IUnitOfWork unitOfWork, IUserContextService userContextService)
		{
			_userManager = userManager;
			_uow = unitOfWork;
			_userContextService = userContextService;
		}
		

		[HttpGet]
		public async Task<IActionResult> Get([FromQuery] PaginationFilter filter)
		{
			var usuarios = await _uow.UsuarioRepository.GetListPaginateAsync(
				includes: new Expression<System.Func<Usuario, object>>[] { x => x.Rango },
				predicate: x => x.Nombre.Contains(filter.SearchTerm ?? "") || x.Apellido.Contains(filter.SearchTerm ?? "") && x.Institucion == (InstitucionEnum)_codInstitucionUsuario,
				selector: x => new UsuarioModel
				{
					Id = x.Id,
					Cedula = x.Cedula,
					Nombre = x.Nombre,
					Apellido = x.Apellido,
					Rango = x.Rango.Nombre,
					Institucion = Enum.GetName<InstitucionEnum>((InstitucionEnum)x.Institucion)
				},
				page: filter.Page,
				pageSize: filter.Size,
				orderBy: x => x.OrderBy(x => x.Nombre)
			);

			return Ok(usuarios);
		}

		[HttpPost]
        public async Task<IActionResult> Create([FromBody] UsuarioRegisterDto registerDto)
        {
			if (await _userManager.FindByNameAsync(registerDto.Username) != null)
			{
				throw new BaseException("El usuario ya existe", HttpStatusCode.BadRequest);
			}

			if (await _userManager.FindByEmailAsync(registerDto.Cedula) != null)
			{
				throw new BaseException("El usuario ya existe", HttpStatusCode.BadRequest);
			}

            var usuario = new Usuario
            {
                Cedula = registerDto.Cedula,
                Nombre = registerDto.Nombre,
                Apellido = registerDto.Apellido,
                RangoId = registerDto.RangoId,
                Institucion = (InstitucionEnum)_codInstitucionUsuario,
                UserName = registerDto.Username
            };

            if (!(await _userManager.CreateAsync(usuario, registerDto.Password)).Succeeded)
            {
                throw new BaseException("Error registrando usuario", HttpStatusCode.BadRequest);
            }


			var roles = 
				await _uow.RoleRepository.GetListAsync(predicate: r => registerDto.Roles.Contains(r.Id), selector: r => r.Name) 
				?? throw new BaseException("No se encontraron roles para asignar", HttpStatusCode.NotFound);

            if(!(await _userManager.AddToRolesAsync(usuario, roles)).Succeeded)
            {
                throw new BaseException("Error asignando roles", HttpStatusCode.BadRequest);
            }

            return Ok();
        }

	}

	
}
