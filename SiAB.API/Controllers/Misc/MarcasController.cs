using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiAB.Application.Contracts;
using SiAB.Core.Entities.Misc;

namespace SiAB.API.Controllers.Misc
{
    [ApiController]
	[Route("api/marcas")]
	public class MarcasController : NamedController<Marca>
	{
		public MarcasController(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
			
		}

	}
}
