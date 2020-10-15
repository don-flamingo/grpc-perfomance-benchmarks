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
        private readonly HttpClient _httpClient;
        private readonly GrpcChannel _channel;
        private readonly IBigDtoService _service;

        public Communication()
        {
            GrpcClientFactory.AllowUnencryptedHttp2 = true;

            _channel = GrpcChannel.ForAddress("http://localhost:5000");
            _service = _channel.CreateGrpcService<IBigDtoService>();
            _httpClient = CreateClient();
        }
    }
}