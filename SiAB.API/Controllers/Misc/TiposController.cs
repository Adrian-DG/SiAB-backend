using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiAB.Application.Contracts;
using SiAB.Core.Entities.Misc;

namespace SiAB.API.Controllers.Misc
{
	[Route("api/tipos")]
	[ApiController]
	public class TiposController : GenericController<Tipo>
	{
		public TiposController(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}
	}
}
