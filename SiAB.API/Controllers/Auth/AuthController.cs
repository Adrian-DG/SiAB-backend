using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SiAB.API.Services;
using SiAB.Core.DTO.Auth;
using SiAB.Core.Entities.Auth;
using SiAB.Core.Exceptions;

namespace SiAB.API.Controllers.Auth
{

    [ApiController]
    [Route("api/authentication")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly JwtService _jwtService;
        public AuthController(UserManager<Usuario> userManager, JwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        [HttpPost("register-user")]
        public async Task<IActionResult> Register([FromBody] UsuarioRegisterDto registerDto)
        {
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

            return Created("register-user", true);
        }

        [HttpPost("login-user")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDto loginDto)
        {
            var usuario = await _userManager.FindByNameAsync(loginDto.Username);

            if (usuario is null) throw new BaseException("El usuario no existe", System.Net.HttpStatusCode.NotFound);

            if (!await _userManager.CheckPasswordAsync(usuario, loginDto.Password)) throw new BaseException("Credenciales invalidas", System.Net.HttpStatusCode.BadRequest);

            var roles = await _userManager.GetRolesAsync(usuario);
            
            var token = _jwtService.CreateToken(usuario, roles);

            return Ok(token);
        }

        [HttpGet("is-authenticated")]
        public IActionResult IsAuthenticated()
        {
            return Ok("Authenticated");
        }

    }
}
