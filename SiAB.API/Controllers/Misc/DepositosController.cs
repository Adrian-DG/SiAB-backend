using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiAB.Application.Contracts;
using SiAB.Core.Entities.Misc;

namespace SiAB.API.Controllers.Misc
{
	[Route("api/depositos")]
	[ApiController]
	public class DepositosController : GenericController<Deposito>
	{
		public DepositosController(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}
	}
}
