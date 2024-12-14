using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiAB.Application.Contracts;
using SiAB.Core.Entities.Misc;

namespace SiAB.API.Controllers.Misc
{
    [Route("api/modelos")]
	public class ModelosController : GenericController<Modelo>
	{
		public ModelosController(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}
	}
}
