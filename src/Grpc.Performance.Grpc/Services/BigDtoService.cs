using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Perfomance.Contracts.Big;
using Grpc.Performance.Application;
using Grpc.Performance.Contracts.Big;

namespace Grpc.Perfomance.Grpc.Services
{
    public class BigDtoService : IBigDtoService
    {
        private readonly IBigEntityDtoRepository _repository;
        
        public BigDtoService(IBigEntityDtoRepository repository)
        {
            _repository = repository;
        }

        public async ValueTask<BigDto> GetBigDtoAsync(CancellationToken cancellationToken)
        {
            var items = await _repository.GetAsync();
            var item = items.First();

            return item;
        }

        public async ValueTask<ICollection<BigDto>> GetBigDtosAsync(CancellationToken cancellationToken)
            => await _repository.GetAsync();

        public async IAsyncEnumerable<BigDto> GetBigDtosStreamAsync(BigDtoQuery query,
            CancellationToken cancellationToken)
        {
            foreach (var item in _repository.Get())
            {
                yield return item;
            }
        }

        public ValueTask ThrowExceptionAsync(CancellationToken cancellationToken)
        {
            throw new Exception("Error");
        }
    }
}