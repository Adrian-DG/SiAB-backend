using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiAB.Application.Contracts;

namespace SiAB.API.Controllers.JCE
{
	[Route("api/junta-central-electoral")]
	[ApiController]
	public class JCEController : ControllerBase
	{
		private readonly IUnitOfWork _uow;
		private readonly IMapper _mapper;	
		public JCEController(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_uow = unitOfWork;
			_mapper = mapper;
		}

		[HttpGet("{cedula}")]
		public IActionResult Get([FromRoute] string cedula)
		{
			var result = _uow.JCERepository.GetInfoCivilByCedula(cedula);

			if (result == new Core.Models.JCE.JCEResult())
			{
				return NotFound();
			}

			return new JsonResult(result);
		}
	}
}
