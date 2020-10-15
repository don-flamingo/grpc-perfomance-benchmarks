using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Grpc.Net.Client;
using Grpc.Perfomance.Contracts.Big;
using Grpc.Performance.Contracts.Big;
using ProtoBuf.Grpc.Client;

namespace Grpc.Performance
{
    public class Grpc
    {
        public GrpcChannel _channel;
            
        public Grpc()
        {
            GrpcClientFactory.AllowUnencryptedHttp2 = true;
            _channel = GrpcChannel.ForAddress("http://localhost:5000");
            
        }
            
        [Benchmark]
        public async Task GetBigDtoAsync()
        {
            var service = _channel.CreateGrpcService<IBigDtoService>();
            var collection = await service.GetBigDtosAsync(CancellationToken.None);
        }
        
        [Benchmark]
        public async Task GetBigDtoWithoutChannelAsync()
        {
            GrpcClientFactory.AllowUnencryptedHttp2 = true;
            var channel = GrpcChannel.ForAddress("http://localhost:5000");
            var service = channel.CreateGrpcService<IBigDtoService>();
            var collection = await service.GetBigDtosAsync(CancellationToken.None);
        }
        
        [Benchmark]
        public async Task GetBigDtoStreamAsync()
        {
            var service = _channel.CreateGrpcService<IBigDtoService>();
            var streamAsync = service.GetBigDtosStreamAsync(new BigDtoQuery(),  CancellationToken.None);

            await foreach (var bigDto in streamAsync)
            {
                
            }
        }
    }
}