using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiAB.Application.Contracts;
using SiAB.Core.Entities.Misc;

namespace SiAB.API.Controllers.Misc
{
	[Route("api/articulos")]
	[ApiController]
	public class ArticulosController : NamedController<Articulo>
	{
		public ArticulosController(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}
	}
}
