using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Grpc.Performance.Contracts.Big;
using Grpc.Performance.Fixtures;

namespace Grpc.Performance.Benchmarks
{
    public class Concurrency
    {
        private readonly InfrastructureFixture _infrastructure;

        [Params(10, 25, 100)]
        public int Threads { get; set; }

        public Concurrency()
        {
            _infrastructure = new InfrastructureFixture();
        }

        [Benchmark]
        [BenchmarkCategory(Categories.Grpc)]
        public async Task GrpcCodeFirstGetConcurrencyAsync()
        {
            var tasks = new List<Task<BigDto>>();

            for (var i = 0; i < Threads; i++)
            {
                var task = _infrastructure.CodeFirstService.GetBigDtoAsync(CancellationToken.None);
                tasks.Add(task);
            }

            await Task.WhenAll(tasks);
        }
        

        
        [Benchmark]
        [BenchmarkCategory(Categories.Rest)]
        public async Task RestGetConcurrencyAsync()
        {
            var tasks = new List<Task>();
            
            for (var i = 0; i < Threads; i++)
            {
                var task = RestGetBigDtoAsync();
                tasks.Add(task);
            }

            await Task.WhenAll(tasks);
        }

        private async Task<BigDto> RestGetBigDtoAsync()
        {
            var response = await _infrastructure.HttpClient.GetAsync("big/item");
            var json = await response.Content.ReadAsStringAsync();
            var item = JsonSerializer.Deserialize<BigDto>(json);

            return item;
        }
    }
}