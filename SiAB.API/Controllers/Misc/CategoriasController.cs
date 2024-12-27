﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiAB.API.Helpers;
using SiAB.Application.Contracts;
using SiAB.Core.DTO;
using SiAB.Core.Entities.Misc;
using SiAB.Core.Models;

namespace SiAB.API.Controllers.Misc
{
	[Route("api/categorias")]
	[ApiController]
	public class CategoriasController : GenericController
	{
		public CategoriasController(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService) : base(unitOfWork, mapper, userContextService)
		{
		}

		[HttpGet]
		public async Task<IActionResult> Get([FromQuery] PaginationFilter filter)
		{
			var categorias = await _uow.Repository<Categoria>().GetListPaginateAsync(
					predicate: c => c.Nombre.Contains(filter.SearchTerm ?? ""),	
					selector: c => new NamedModel
					{
						Id = c.Id,
						Nombre = c.Nombre						
					},
					page: filter.Page,
					pageSize: filter.Size
				);
			
			return new JsonResult(categorias);
		}
	}
}
