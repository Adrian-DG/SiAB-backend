﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiAB.Application.Contracts;
using SiAB.Core.Entities.Misc;

namespace SiAB.API.Controllers.Misc
{
	[ApiController]
	[Route("api/marcas")]
	public class MarcasController : GenericController<Marca>
	{
		public MarcasController(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
			
		}

		[HttpGet("have-access")]
		public ActionResult MarcasAuthorize()
		{
			return Ok("Authorized");
		}
	}
}
