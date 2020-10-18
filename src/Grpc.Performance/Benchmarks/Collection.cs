using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Grpc.Performance.Fixtures;

namespace Grpc.Performance.Benchmarks
{
    [RPlotExporter]
    public class Collection
    {
        private readonly InfrastructureFixture _infrastructure;
        
        public Collection()
        {
            _infrastructure = new InfrastructureFixture();
        }
        
        [Benchmark]
        [BenchmarkCategory(Categories.Grpc)]
        public async Task GrpcGetCollectionBigDtoAsync()
        {
            var collection = await _infrastructure.CodeFirstService.GetBigDtosAsync(CancellationToken.None);
        }
        
        [Benchmark]
        [BenchmarkCategory(Categories.Grpc)]
        public async Task GrpcGetPaginatedBigDtoAsync()
        {
            var collection = await _infrastructure.CodeFirstService.GetPaginatedAsync(CancellationToken.None);
        }
        
        [Benchmark]
        [BenchmarkCategory(Categories.Rest)]
        public async Task RestGetCollectionBigDtoAsync()
        {
            var collection = await _infrastructure.RestGetBigCollectionAsync();
        }
        
        [Benchmark]
        [BenchmarkCategory(Categories.Rest)]
        public async Task RestGetPaginatedBigDtoAsync()
        {
            var collection = await _infrastructure.RestGetBigPaginatedAsync();
        }
    }
}