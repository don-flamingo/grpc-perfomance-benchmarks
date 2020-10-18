using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Grpc.Performance.Contracts.Big;
using Grpc.Performance.Grpc.Proto;
using ProtoBuf.Grpc.Client;
using BigDto = Grpc.Perfomance.Contracts.Big.BigDto;

namespace Grpc.Performance.Fixtures
{
    public class InfrastructureFixture
    {
        public HttpClient HttpClient { get; }
        public GrpcChannel Channel { get; }
        public IBigService CodeFirstService { get; }
        public BigDtoService.BigDtoServiceClient ContractFirstService { get; }
        

        public InfrastructureFixture()
        {
            GrpcClientFactory.AllowUnencryptedHttp2 = true;

            Channel = GrpcChannel.ForAddress("http://localhost:5000");
            CodeFirstService = Channel.CreateGrpcService<IBigService>();
            ContractFirstService = new BigDtoService.BigDtoServiceClient(Channel);
            HttpClient = CreateClient();
        }
        
        private HttpClient CreateClient()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => true
            };

            return new HttpClient(handler)
            {
                BaseAddress = new Uri("https://localhost:6001/")
            };
        }
        
        public async Task<BigDto> RestGetBigDtoAsync()
        {
            var response = await HttpClient.GetAsync("big/item");
            var json = await response.Content.ReadAsStringAsync();
            var item = JsonSerializer.Deserialize<BigDto>(json);

            return item;
        }
    }
}