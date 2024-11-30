using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiAB.Application.Contracts;
using SiAB.Core.Abstraction;
using SiAB.Core.DTO;
using SiAB.Core.Models;
using System.Linq.Expressions;

namespace SiAB.API.Controllers
{	
	[ApiController]
	[Route("api/[controller]")]
	public abstract class GenericController<T> : ControllerBase where T : NamedMetadata
	{
		protected readonly IUnitOfWork _uow;
		protected GenericController(IUnitOfWork unitOfWork)
		{
			_uow = unitOfWork;
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] string Nombre)
		{
			T entity = (T) new NamedMetadata() { Nombre = Nombre };	
			await _uow.Repository<T>().AddAsync(entity);
			return Created();
		}

		[HttpGet]
		public async Task<IActionResult> Get([FromQuery] PaginationFilter filter)
		{
			var result = await _uow.Repository<T>().GetListPaginateAsync(
				x => x.Nombre.Contains(filter.SearchTerm), 
				e => new NamedModel { Id = e.Id, Nombre = e.Nombre },
				page: filter.Page,
				pageSize: filter.Size,
				orderBy: e => e.OrderBy(p => p.Nombre)
			);

			return new JsonResult(result);			
		}

		[HttpPut("{id:int}")]
		public async Task<IActionResult> Update([FromRoute] int id, [FromBody] string Nombre)
		{
			var entity = await _uow.Repository<T>().GetByIdAsync(id);
			entity.Nombre = Nombre;
			await _uow.Repository<T>().Update(entity);
			return Ok();
		}
		
	}
}
