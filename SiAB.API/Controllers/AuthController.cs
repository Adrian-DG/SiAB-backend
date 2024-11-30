using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SiAB.API.Services;
using SiAB.Core.DTO.Auth;
using SiAB.Core.Entities.Auth;
using SiAB.Core.Exceptions;

namespace SiAB.API.Controllers
{

	[ApiController]
	[AllowAnonymous]
	[Route("api/authentication")]
	public class AuthController : ControllerBase
	{
		private readonly UserManager<Usuario> _userManager;
		private readonly JwtService _jwtService;
		private readonly IMapper _mapper;
		public AuthController(UserManager<Usuario> userManager, JwtService jwtService, IMapper mapper)
		{
			_userManager = userManager;
			_jwtService = jwtService;
			_mapper = mapper;
		}

		[HttpPost("register-user")]
		public async Task<IActionResult> Register([FromBody] UsuarioRegisterDto registerDto)
		{
			var usuario = _mapper.Map<Usuario>(registerDto);

			var result = await _userManager.CreateAsync(usuario, registerDto.Password);

			if (!result.Succeeded) throw new BaseException("Error registrando usuario");

			return Created("register-user", result.Succeeded);
		}

		[HttpPost("login-user")]
		public async Task<IActionResult> Login([FromBody] UsuarioLoginDto loginDto)
		{
			var usuario = await _userManager.FindByNameAsync(loginDto.Username);

			if (usuario is null) throw new BaseException("El usuario no existe", System.Net.HttpStatusCode.NotFound);

			if (!await _userManager.CheckPasswordAsync(usuario, loginDto.Password)) throw new BaseException("Credenciales invalidas", System.Net.HttpStatusCode.BadRequest);

			var token = _jwtService.CreateToken(usuario);

			return Ok(token);			
		}

		[Authorize]
		[HttpGet("is-authenticated")]
		public IActionResult IsAuthenticated()
		{
			return Ok();
		}

	}
}
