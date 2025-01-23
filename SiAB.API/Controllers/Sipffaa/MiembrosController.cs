using Microsoft.AspNetCore.Mvc;
using SiAB.Application.Contracts;

namespace SiAB.API.Controllers.Sipffaa
{
    [Route("api/miembros")]
    [ApiController]
    public class MiembrosController : SipffaaController
    {
        public MiembrosController(ISipffaaRepository repository) : base(repository)
        {
        }


		/// <summary>
		/// Obtiene una lista de miembros filtrados por c�dula.
		/// </summary>
		/// <param name="cedula">La c�dula del miembro.</param>
		/// <returns>Una lista de miembros que coinciden con la c�dula proporcionada.</returns>
		[HttpGet("filtrar-miembros-por-cedula-nombre")]
		public async Task<IActionResult> GetMiembrosByCedula([FromQuery] string param)
		{
			if (string.IsNullOrEmpty(param)) return BadRequest();

			var miembros = await _repository.GetMiembrosByCedulaNombre(param.Replace("-", ""));

			return new JsonResult(miembros);
		}

		/// <summary>
		/// Obtiene un miembro espec�fico por c�dula.
		/// </summary>
		/// <param name="cedula">La c�dula del miembro.</param>
		/// <returns>El miembro que coincide con la c�dula proporcionada.</returns>
		[HttpGet("filtro-cedula/{cedula}")]
		public async Task<IActionResult> GetMiembroByCedula([FromRoute] string cedula)
		{
			if (string.IsNullOrEmpty(cedula)) return BadRequest();

			var miembro = await _repository.GetMiembroByCedula(cedula.Replace("-", ""));

			if (miembro is null)
			{
				return NotFound();
			}

			return Ok(miembro);
		}
       
    }
}
