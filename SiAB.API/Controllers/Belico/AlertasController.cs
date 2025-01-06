using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiAB.API.Helpers;
using SiAB.Application.Contracts;
using SiAB.Core.Entities.Belico;

namespace SiAB.API.Controllers.Belico
{
	[Route("api/alertas")]
	[ApiController]
	public class AlertasController : GenericController
	{
		public AlertasController(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService) : base(unitOfWork, mapper, userContextService)
		{
		}
	}
}
