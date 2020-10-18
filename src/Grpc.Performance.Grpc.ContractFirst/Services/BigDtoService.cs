using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Performance.Application;

namespace Grpc.Performance.Grpc.Proto.Services
{
    public class BigDtoService : Proto.BigDtoService.BigDtoServiceBase
    {
        private readonly IBigEntityDtoRepository _repository;
        
        public BigDtoService(IBigEntityDtoRepository repository)
        {
            _repository = repository;
        }
        public override async Task<BigDto> GetBigDto(Empty request, ServerCallContext context)
        {
            var items = await _repository.GetAsync();
            var item = items.First();
            
            return item;
        }
    }
}