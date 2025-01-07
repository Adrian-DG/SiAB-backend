using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiAB.API.Helpers;
using SiAB.Application.Contracts;
using SiAB.Core.DTO;
using SiAB.Core.DTO.Auth;
using SiAB.Core.Entities.Auth;
using SiAB.Core.Enums;
using SiAB.Core.Exceptions;

namespace SiAB.API.Controllers.Auth
{
	[Route("api/usuarios")]
	[ApiController]
	public class UsuariosController : GenericController
	{
		private readonly UserManager<Usuario> _userManager;
        public UsuariosController(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService, UserManager<Usuario> userManager) : base(unitOfWork, mapper, userContextService)
        {
			_userManager = userManager;
        }

        [HttpGet]
		public async Task<IActionResult> Get([FromQuery] PaginationFilter filter)
		{
			var usuarios = await _userManager.Users
				.Where(u => u.Cedula.Contains(filter.SearchTerm) || String.Concat(u.Nombre, " ", u.Apellido).Contains(filter.SearchTerm) && u.Institucion == (InstitucionEnum)_codInstitucionUsuario)
				.Select(u => new {
					Id = u.Id,
					Cedula = u.Cedula,
					NombreCompleto = String.Concat(u.Nombre, " ", u.Apellido),
					Rango = u.Rango.Nombre,
					Institucion = u.Institucion.ToString(),
				})
				.Skip((filter.Page -1) * filter.Size)
				.Take(filter.Size).
				ToListAsync();

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
                Institucion = registerDto.InstitucionEnum,
                UserName = registerDto.Username
            };

            if (!(await _userManager.CreateAsync(usuario, registerDto.Password)).Succeeded)
            {
                throw new BaseException("Error registrando usuario", HttpStatusCode.BadRequest);
            }

            if(!(await _userManager.AddToRolesAsync(usuario, registerDto.Roles)).Succeeded)
            {
                throw new BaseException("Error asignando roles", HttpStatusCode.BadRequest);
            }

            return Ok();
        }

	}
}
