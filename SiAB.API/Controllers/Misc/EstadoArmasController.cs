using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiAB.API.Helpers;
using SiAB.Application.Contracts;
using SiAB.Core.DTO;
using SiAB.Core.Entities.Misc;
using SiAB.Core.Models;
using System.Linq.Expressions;

namespace SiAB.API.Controllers.Misc
{
	[Route("api/estado-armas")]
	[ApiController]
	public class EstadoArmasController : GenericController
	{
		public EstadoArmasController(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService) : base(unitOfWork, mapper, userContextService)
		{
		}
		
	}
}
