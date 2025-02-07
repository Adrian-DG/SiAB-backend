using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SiAB.API.Helpers;
using SiAB.Application.Contracts;
using SiAB.Core.Entities.Inventario;

namespace SiAB.API.Controllers.Inventario
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
