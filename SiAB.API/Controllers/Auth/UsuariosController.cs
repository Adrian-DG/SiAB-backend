using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiAB.Application.Contracts;
using SiAB.Core.DTO;
using SiAB.Core.Entities.Auth;

namespace SiAB.API.Controllers.Auth
{
	[Route("api/usuarios")]
	[ApiController]
	public class UsuariosController : ControllerBase
	{
		private readonly UserManager<Usuario> _userManager;
		public UsuariosController(UserManager<Usuario> userManager)
		{
			_userManager = userManager;
		}

		[HttpGet]
		public async Task<IActionResult> Get([FromQuery] PaginationFilter filter)
		{
			var usuarios = await _userManager.Users
				.Where(u => u.Cedula.Contains(filter.SearchTerm) || String.Concat(u.Nombre, " ", u.Apellido).Contains(filter.SearchTerm))
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
	}
}
