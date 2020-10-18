
using System.Linq;
using System.Threading.Tasks;
using Grpc.Performance.Application;
using Grpc.Performance.Contracts.Big;
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
        
        [HttpPost]
        public async Task<IActionResult> Send([FromBody] CreateBigCommand command)
        {
            return Ok();
        }
        
        [HttpPost("events")]
        public async Task<IActionResult> Send([FromBody] CreateBigEventsCommand command)
        {
            return Ok();
        }
    }
}