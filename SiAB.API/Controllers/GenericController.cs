using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
	public abstract class GenericController<T> : ControllerBase where T : EntityMetadata
	{
		protected readonly IUnitOfWork _uow;
		public GenericController(IUnitOfWork unitOfWork)
		{
			_uow = unitOfWork;
		}
		
		[HttpGet]
		public async Task<IActionResult> Get([FromQuery] PaginationFilter filter)
		{
			var result = await _uow.Repository<T>().GetListPaginateAsync(
					selector: x => new T{}
					page: filter.Page,
					pageSize: filter.Size,
					orderBy: x => x.OrderBy(x => x.Id)
				);
			
			return new JsonResult(result);
		}
		{
			var result = await _uow.Repository<T>().GetListPaginateAsync(
					predicate: x => x.Nombre.Contains(filter.SearchTerm ?? ""),
					selector: x => new { x.Id, x.Nombre },
					page: filter.Page,
					pageSize: filter.Size,
					orderBy: x => x.OrderBy(x => x.Id)
				);
			
			return new JsonResult(result);
		}
	}
}
