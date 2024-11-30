using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiAB.Application.Contracts;

namespace SiAB.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BaseController : ControllerBase
	{
		protected readonly IUnitOfWork _uow;
		public BaseController(IUnitOfWork unitOfWork)
		{
			_uow = unitOfWork;
		}
	}
}
