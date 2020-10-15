using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Performance.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Grpc.Performance.Rest.Controllers
{
    [ApiController]
    [Route("big-dto")]
    public class BigDtoController : ControllerBase
    {
        private IBigEntityDtoRepository _repository;

        public BigDtoController(IBigEntityDtoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var items = await _repository.GetAsync();
            return Ok(items);
        }
    }
}