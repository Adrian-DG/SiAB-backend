using Microsoft.AspNetCore.Mvc;
using SiAB.Application.Contracts;
using SiAB.Core.Abstraction;
using SiAB.Core.DTO;
using SiAB.Core.Models;

namespace SiAB.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class NamedController<T> : ControllerBase where T : NamedMetadata
    {
        protected readonly IUnitOfWork _uow;
        
        public NamedController(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationFilter filter)
        {
            var result = await _uow.Repository<T>().GetListPaginateAsync(
                predicate: x => x.Nombre.Contains(filter.SearchTerm ?? ""),
                selector: x => new NamedModel { Id = x.Id, Nombre = x.Nombre},
                page: filter.Page,
                pageSize: filter.Size
            );

            return new JsonResult(result);
        }
    }
}
