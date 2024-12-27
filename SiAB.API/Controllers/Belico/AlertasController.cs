using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiAB.Application.Contracts;
using SiAB.Core.Entities.Belico;

namespace SiAB.API.Controllers.Belico
{
	[Route("api/alertas")]
	[ApiController]
	public class AlertasController : GenericController
	{
		public AlertasController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
		{
		}
	}
}
