using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiAB.API.Helpers;
using SiAB.Application.Contracts;
using SiAB.Core.Entities.Personal;

namespace SiAB.API.Controllers.Misc
{
	[Route("api/rangos")]
	public class RangosController : GenericController<Rango>
	{
		public RangosController(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService) : base(unitOfWork, mapper, userContextService)
		{
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var rangos = new List<Rango>() {
					new Rango { Id = 1, Nombre = "MAYOR GENERAL", NombreArmada = "VICEALMIRANTE" },
					new Rango { Id = 2, Nombre = "GENERAL", NombreArmada = "CONTRALMIRANTE" },
					new Rango { Id = 3, Nombre = "CORONEL", NombreArmada = "CAPITAN DE NAVIO" },
					new Rango { Id = 4, Nombre = "TENIENTE CORONEL", NombreArmada = "CAPITAN DE FRAGATA" },
					new Rango { Id = 5, Nombre = "MAYOR", NombreArmada = "CAPITAN CORBETA" },
					new Rango { Id = 6, Nombre = "CAPITAN", NombreArmada = "TENIENTE NAVIO" },
					new Rango { Id = 7, Nombre = "1ER TENIENTE", NombreArmada = "TENIENTE FRAGATA" },
					new Rango { Id = 8, Nombre = "2DO TENIENTE/TENIENTE CORBETA", NombreArmada = "TENIENTE CORBETA" },
					new Rango { Id = 9, Nombre = "SARGENTO MAYOR", NombreArmada = "SARGENTO MAYOR" },
					new Rango { Id = 10, Nombre = "SARGENTO", NombreArmada = "SARGENTO" },
					new Rango { Id = 11, Nombre = "CABO", NombreArmada = "CABO" },
					new Rango { Id = 12, Nombre = "RASO/MARINERO", NombreArmada = "MARINERO" },
					new Rango { Id = 13, Nombre = "ASIMILADO", NombreArmada = "ASIMILADO" }
			}.Select(r => new Rango
			{
				Id = r.Id,
				Nombre = String.Concat(r.Nombre, "/", r.NombreArmada)
			}).OrderBy(r => r.Id).ToList();

			return new JsonResult(rangos);
		}
	}
}
