using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiAB.Application.Contracts;
using SiAB.Core.Entities.Belico;

namespace SiAB.API.Controllers.Belico
{
	[Route("api/calibres")]
	[ApiController]
	public class CalibresController : GenericController<Calibre>
	{
		public CalibresController(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}
	}
}
