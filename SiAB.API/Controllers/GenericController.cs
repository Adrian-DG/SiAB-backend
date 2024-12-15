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
		
	}
}
