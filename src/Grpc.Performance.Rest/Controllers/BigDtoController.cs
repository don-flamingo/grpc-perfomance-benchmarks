
using System.Linq;
using System.Threading.Tasks;
using Grpc.Performance.Application;
using Microsoft.AspNetCore.Mvc;

namespace Grpc.Performance.Rest.Controllers
{
    [ApiController]
    [Route("big")]
    public class BigDtoController : ControllerBase
    {
        private IBigRepository _repository;

        public BigDtoController(IBigRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var items = await _repository.GetAsync();
            return Ok(items);
        }
        
        [HttpGet("item")]
        public async Task<IActionResult> GetItem()
        {
            var items = await _repository.GetAsync();
            var item = items.First();
            return Ok(item);
        }
    }
}