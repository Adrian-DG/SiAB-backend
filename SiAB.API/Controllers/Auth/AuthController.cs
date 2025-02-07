using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SiAB.API.Attributes;
using SiAB.API.Services;
using SiAB.Core.DTO.Auth;
using SiAB.Core.Entities.Auth;
using SiAB.Core.Enums;
using SiAB.Core.Exceptions;

namespace SiAB.API.Controllers.Auth
{

    [ApiController]
    [Route("api/authentication")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IConfiguration _configuration;
        private readonly JwtService _jwtService;
        public AuthController(UserManager<Usuario> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;

            var jwtKey = configuration["Jwt:Key"];
            
            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new ArgumentNullException(nameof(jwtKey), "JWT Key cannot be null or empty.");
            }
			_jwtService = new JwtService(jwtKey);
		}
        
        [HttpPost("login-user")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDto loginDto)
        {
            var usuario = await _userManager.FindByNameAsync(loginDto.Username);

            if (usuario is null) throw new BaseException("El usuario no existe", System.Net.HttpStatusCode.NotFound);

            if (!await _userManager.CheckPasswordAsync(usuario, loginDto.Password)) throw new BaseException("Credenciales invalidas", System.Net.HttpStatusCode.BadRequest);

            var roles = await _userManager.GetRolesAsync(usuario);
            
            var token = _jwtService.GenerateToken(usuario, roles.ToArray());

            return Ok(token);
        }

        [AuthorizeRole(UsuarioRolesEnum.ADMINISTRADOR_GENERAL)]
		[HttpGet("is-authenticated")]
        public IActionResult IsAuthenticated()
        {
            return Ok("Authenticated");
        }

    }
}
