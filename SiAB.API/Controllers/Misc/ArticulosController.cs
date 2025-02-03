using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiAB.API.Helpers;
using SiAB.Application.Contracts;
using SiAB.Core.Entities.Belico;
using SiAB.Core.Enums;

namespace SiAB.API.Controllers.Misc
{
	[Route("api/articulos")]
	[ApiController]
	public class ArticulosController : GenericController<Articulo>
	{
		public ArticulosController(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService) : base(unitOfWork, mapper, userContextService)
		{
		}
	}
}
