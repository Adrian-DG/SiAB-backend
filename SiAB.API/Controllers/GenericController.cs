using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiAB.API.Attributes;
using SiAB.Application.Contracts;
using SiAB.Core.Abstraction;
using SiAB.Core.DTO;
using SiAB.Core.Models;
using System.Linq.Expressions;

namespace SiAB.API.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public abstract class GenericController<T> : ControllerBase where T : EntityMetadata
	{
		protected readonly IUnitOfWork _uow;
		protected readonly IMapper _mapper;
		public GenericController(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_uow = unitOfWork;
			_mapper = mapper;
		}

		[HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			var entity = await _uow.Repository<T>().GetByIdAsync(id);

			if (entity is null)
			{
				return NotFound();
			}

			entity.IsDeleted = true;
			entity.FechaModificacion = DateTime.Now;

			await _uow.Repository<T>().Update(entity);

			return NoContent();
		}

	}	
}
