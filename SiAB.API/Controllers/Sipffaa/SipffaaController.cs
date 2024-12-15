using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiAB.Application.Contracts;

namespace SiAB.API.Controllers.Sipffaa
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class SipffaaController : ControllerBase
    {
        protected readonly ISipffaaRepository _repository;

        public SipffaaController(ISipffaaRepository repository)
        {
            _repository = repository;
        }
       
    }
}
