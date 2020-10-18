using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Grpc.Performance.Fixtures;

namespace Grpc.Performance.Benchmarks
{
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
        [BenchmarkCategory(Categories.Rest)]
        public async Task RestGetCollectionBigDtoAsync()
        {
            var collection = await _infrastructure.RestGetBigCollectionAsync();
        }
    }
}