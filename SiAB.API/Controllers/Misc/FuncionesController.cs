using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiAB.Application.Contracts;
using SiAB.Core.Entities.Personal;

namespace SiAB.API.Controllers.Misc
{
	[Route("api/funciones")]
	[ApiController]
	public class FuncionesController : GenericController<Funcion>
	{
		public FuncionesController(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}
	}
}
