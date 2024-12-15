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

        [HttpGet("{cedula}")]
        public async Task<IActionResult> GetMiembroByCedula([FromRoute] string cedula)
        {
            var miembro = await _repository.GetMiembroByCedula(cedula);
            
            if (miembro is null)
            {
                return NotFound();
            }
            
            return Ok(miembro);
        }
       
    }
}
