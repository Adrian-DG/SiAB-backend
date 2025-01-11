using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SiAB.API.Attributes;
using SiAB.API.Filters;
using SiAB.API.Helpers;
using SiAB.Application.Contracts;
using SiAB.Core.Abstraction;
using SiAB.Core.DTO;
using SiAB.Core.Entities.Auth;
using SiAB.Core.Models;
using System.Linq.Expressions;

namespace SiAB.API.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	[TypeFilter(typeof(CodUsuarioFilter))]
	[TypeFilter(typeof(CodInstitucionFilter))]
	public abstract class GenericController<T> : ControllerBase where T : EntityMetadata
	{
		protected readonly IUnitOfWork _uow;
		protected readonly IMapper _mapper;
		private readonly IUserContextService _userContextService;

		public int _codUsuario
		{
			get => _userContextService.CodUsuario;
			set => _userContextService.CodUsuario = value;
		}

		public int _codInstitucionUsuario
		{
			get => _userContextService.CodInstitucionUsuario;
			set => _userContextService.CodInstitucionUsuario = value;
		}
		public GenericController(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService)
		{
			_uow = unitOfWork;
			_mapper = mapper;
			_userContextService = userContextService;
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

			await _uow.Repository<T>().Update(entity);

			return Ok();
		}

	}	
}
