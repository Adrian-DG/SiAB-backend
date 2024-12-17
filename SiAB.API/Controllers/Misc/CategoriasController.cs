using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiAB.Application.Contracts;
using SiAB.Core.Entities.Misc;

namespace SiAB.API.Controllers.Misc
{
	[Route("api/categorias")]
	[ApiController]
	public class CategoriasController : GenericController<Categoria>
	{
		public CategoriasController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
		{
		}
	}
}
