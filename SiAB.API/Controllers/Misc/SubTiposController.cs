using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiAB.Application.Contracts;
using SiAB.Core.Entities.Misc;

namespace SiAB.API.Controllers.Misc
{
	[Route("api/subtipos")]
	[ApiController]
	public class SubTiposController : GenericController<SubTipo>
	{
		public SubTiposController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
		{
		}
	}
}
