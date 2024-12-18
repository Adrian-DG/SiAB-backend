using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiAB.Application.Contracts;
using SiAB.Core.Entities.Belico;

namespace SiAB.API.Controllers.Belico
{
	[Route("api/series")]
	[ApiController]
	public class SeriesController : GenericController<Serie>
	{
		public SeriesController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
		{
		}
	}
}
