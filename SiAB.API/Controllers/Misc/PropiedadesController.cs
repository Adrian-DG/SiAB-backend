using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiAB.Application.Contracts;
using SiAB.Core.Entities.Misc;

namespace SiAB.API.Controllers.Misc
{
	[Route("api/propiedades")]
	[ApiController]
	public class PropiedadesController : NamedController<Propiedad>
	{
		public PropiedadesController(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}
	}
}
