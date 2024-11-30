using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiAB.Application.Contracts;
using SiAB.Core.Entities.Misc;
using System.Linq.Expressions;

namespace SiAB.API.Controllers.Misc
{
	[Route("api/marcas")]
	public class MarcasController : GenericController<Marca>
	{
		public MarcasController(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
			
		}
	}
}
