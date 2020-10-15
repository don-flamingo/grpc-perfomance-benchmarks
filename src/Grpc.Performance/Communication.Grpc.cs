using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Grpc.Net.Client;
using Grpc.Perfomance.Contracts.Big;
using Grpc.Performance.Contracts.Big;
using ProtoBuf.Grpc.Client;

namespace Grpc.Performance
{
    public partial class Communication
    {

        [Benchmark]
        public async Task GrpcGetCollectionBigDtoAsync()
        {
            var collection = await _service.GetBigDtosAsync(CancellationToken.None);
        }
        
        [Benchmark]
        public async Task GrpcGetCollectionBigDtoWithoutServiceAsync()
        {
            var service = _channel.CreateGrpcService<IBigDtoService>();
            var collection = await service.GetBigDtosAsync(CancellationToken.None);
        }
        
        [Benchmark]
        public async Task GrpcGetCollectionBigDtoWithoutChannelAsync()
        {
            GrpcClientFactory.AllowUnencryptedHttp2 = true;
            var channel = GrpcChannel.ForAddress("http://localhost:5000");
            var service = channel.CreateGrpcService<IBigDtoService>();
            var collection = await service.GetBigDtosAsync(CancellationToken.None);
        }
        
        [Benchmark]
        public async Task GrpcGetBigDtoAsync()
        {
            var item = await _service.GetBigDtoAsync(CancellationToken.None);
        }
        
        [Benchmark]
        public async Task GrpcGetBigDtoWithoutServiceAsync()
        {
            var service = _channel.CreateGrpcService<IBigDtoService>();
            var item = await service.GetBigDtoAsync(CancellationToken.None);
        }
        
        [Benchmark]
        public async Task GrpcGetBigDtoWithoutChannelAsync()
        {
            GrpcClientFactory.AllowUnencryptedHttp2 = true;
            var channel = GrpcChannel.ForAddress("http://localhost:5000");
            var service = channel.CreateGrpcService<IBigDtoService>();
            var item = await service.GetBigDtoAsync(CancellationToken.None);
        }
        
        [Benchmark]
        public async Task GrpcGetBigDto3SeriesAsync()
        {
            var item = await _service.GetBigDtoAsync(CancellationToken.None);
            var item2 = await _service.GetBigDtoAsync(CancellationToken.None);
            var item3 = await _service.GetBigDtoAsync(CancellationToken.None);
        }
        
        [Benchmark]
        public async Task GrpcGetBigDto3SeriesWithoutServiceAsync()
        {
            var service = _channel.CreateGrpcService<IBigDtoService>();
            var item = await service.GetBigDtoAsync(CancellationToken.None);
            var item2 = await service.GetBigDtoAsync(CancellationToken.None);
            var item3 = await service.GetBigDtoAsync(CancellationToken.None);
        }
    }
}