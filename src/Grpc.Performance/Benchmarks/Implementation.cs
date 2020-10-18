using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using FluentAssertions;
using Grpc.Performance.Contracts.Big;
using Grpc.Performance.Fixtures;
using ProtoBuf.Grpc.Client;

namespace Grpc.Performance.Benchmarks
{
    public class Implementation
    {
        private readonly InfrastructureFixture _infrastructure;
        
        [Params(1, 10, 20)]
        public int Count { get; set; }

        public Implementation()
        {
            _infrastructure = new InfrastructureFixture();
        }

        [Benchmark]
        [BenchmarkCategory(Categories.Grpc)]
        public async Task GrpcWithChannelAndService()
        {
            for (var i = 0; i < Count; i++)
            {
                var item = await _infrastructure.CodeFirstService.GetBigDtoAsync(CancellationToken.None);
            }
        }
        
        [Benchmark]
        [BenchmarkCategory(Categories.Grpc)]
        public async Task GrpcWithChannel()
        {
            for (var i = 0; i < Count; i++)
            {
                var service = _infrastructure.Channel.CreateGrpcService<IBigService>();
                var item = await service.GetBigDtoAsync(CancellationToken.None);
            }
        }
        
        [Benchmark]
        [BenchmarkCategory(Categories.Grpc)]
        public async Task GrpcFromZero()
        {
            for (var i = 0; i < Count; i++)
            {
                var channel = _infrastructure.CreateChannel();
                var service = channel.CreateGrpcService<IBigService>();
                var item = await service.GetBigDtoAsync(CancellationToken.None);
            }
        }
        
        [Benchmark]
        [BenchmarkCategory(Categories.Rest)]
        public async Task RestFromZero()
        {
            for (var i = 0; i < Count; i++)
            {
                var httpClient = _infrastructure.CreateClient();
                var response = await httpClient.GetAsync("big/item");
                var json = await response.Content.ReadAsStringAsync();
                var item = JsonSerializer.Deserialize<BigDto>(json);

                response.IsSuccessStatusCode.Should().BeTrue();
                item.Should().NotBeNull();

                if (item == null)
                {
                    throw new Exception();
                }
            }
        }
        
        [Benchmark]
        [BenchmarkCategory(Categories.Rest)]
        public async Task RestWithClient()
        {
            for (var i = 0; i < Count; i++)
            {
                await _infrastructure.RestGetBigAsync();
            }
        }
    }
}