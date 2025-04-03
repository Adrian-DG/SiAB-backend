using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiAB.API.Helpers;
using SiAB.Application.Contracts;
using SiAB.Core.Entities.Misc;
using SiAB.Core.Models;

namespace SiAB.API.Controllers.Misc
{
	[Route("api/tipos-documentos")]
	public class TiposDocumentosController : GenericController<TipoDocumento>
	{
		public TiposDocumentosController(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService) : base(unitOfWork, mapper, userContextService)
		{
		}

		[HttpGet("all")]
		public async Task<IActionResult> GetAll()
		{
			var result = (await _uow.Repository<TipoDocumento>().GetAllAsync())
				.Select(t => new NamedModel { Id = t.Id, Nombre = t.Nombre });

			return new JsonResult(result);
		}
	}
}
