using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Grpc.Perfomance.Contracts.Big;
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

      //  [Benchmark]
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
        

        
   //     [Benchmark]
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