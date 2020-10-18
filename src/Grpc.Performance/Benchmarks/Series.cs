using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Google.Protobuf.WellKnownTypes;
using Grpc.Performance.Fixtures;

namespace Grpc.Performance.Benchmarks
{
    public class Series
    {
        private readonly InfrastructureFixture _infrastructure;

        [Params(10, 25, 100)]
        public int Count { get; set; }

        public Series()
        {
            _infrastructure = new InfrastructureFixture();
        }

        
        [Benchmark]
        public async Task GrpcGetDtoSeriesAsync()
        {
            for (var i = 0; i < Count; i++)
            {
                var task = await _infrastructure.CodeFirstService.GetBigDtoAsync(CancellationToken.None);
            }
        }
        
    //    [Benchmark]
        public async Task GrpcContractFirstGetConcurrencyAsync()
        {
            for (var i = 0; i < Count; i++)
            {
                var task = await _infrastructure.ContractFirstService.GetBigDtoAsync(new Empty());
            }
        }

        [Benchmark]
        public async Task RestGetDtoSeriesAsync()
        {
            for (var i = 0; i < Count; i++)
            {
                var task = await _infrastructure.RestGetBigDtoAsync();
            }
        }
    }
}