using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Grpc.Net.Client;
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
            var service = _channel.CreateGrpcService<IBigService>();
            var collection = await service.GetBigDtosAsync(CancellationToken.None);
        }
        
        [Benchmark]
        public async Task GrpcGetCollectionBigDtoWithoutChannelAsync()
        {
            GrpcClientFactory.AllowUnencryptedHttp2 = true;
            var channel = GrpcChannel.ForAddress("http://localhost:5000");
            var service = channel.CreateGrpcService<IBigService>();
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
            var service = _channel.CreateGrpcService<IBigService>();
            var item = await service.GetBigDtoAsync(CancellationToken.None);
        }
        
        [Benchmark]
        public async Task GrpcGetBigDtoWithoutChannelAsync()
        {
            GrpcClientFactory.AllowUnencryptedHttp2 = true;
            var channel = GrpcChannel.ForAddress("http://localhost:5000");
            var service = channel.CreateGrpcService<IBigService>();
            var item = await service.GetBigDtoAsync(CancellationToken.None);
        }
    }
}