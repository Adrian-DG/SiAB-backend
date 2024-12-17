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
		/// Obtiene una lista de miembros filtrados por cédula.
		/// </summary>
		/// <param name="cedula">La cédula del miembro.</param>
		/// <returns>Una lista de miembros que coinciden con la cédula proporcionada.</returns>
		[HttpGet("filtrar-miembros-por-cedula/{cedula}")]
		public async Task<IActionResult> GetMiembrosByCedula([FromRoute] string cedula)
		{
			if (string.IsNullOrEmpty(cedula)) return BadRequest();

			var miembros = await _repository.GetMiembrosByCedula(cedula.Replace("-", ""));

			return new JsonResult(miembros);
		}

		/// <summary>
		/// Obtiene un miembro específico por cédula.
		/// </summary>
		/// <param name="cedula">La cédula del miembro.</param>
		/// <returns>El miembro que coincide con la cédula proporcionada.</returns>
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
