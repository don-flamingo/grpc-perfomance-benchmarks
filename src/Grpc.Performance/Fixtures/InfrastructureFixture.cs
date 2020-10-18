using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Grpc.Net.Client;
using Grpc.Perfomance.Contracts;
using Grpc.Performance.Contracts.Big;
using Grpc.Performance.Grpc.Proto;
using ProtoBuf.Grpc.Client;
using BigDto = Grpc.Performance.Contracts.Big.BigDto;

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

        public GrpcChannel CreateChannel() => GrpcChannel.ForAddress("http://localhost:5000");

        public HttpClient CreateClient()
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
        
        public async Task<BigDto> RestGetBigAsync()
        {
            var response = await HttpClient.GetAsync("big/item");
            var json = await response.Content.ReadAsStringAsync();
            var item = JsonSerializer.Deserialize<BigDto>(json);
            
            return item;
        }
        
        public async Task<ICollection<BigDto>> RestGetBigCollectionAsync()
        {
            var response = await HttpClient.GetAsync("big");
            var json = await response.Content.ReadAsStringAsync();
            var item = JsonSerializer.Deserialize<ICollection<BigDto>>(json);

            return item;
        }
        
        public async Task<PaginationWrapper<BigDto>> RestGetBigPaginatedAsync()
        {
            var response = await HttpClient.GetAsync("paginated");
            var json = await response.Content.ReadAsStringAsync();
            var item = JsonSerializer.Deserialize<PaginationWrapper<BigDto>>(json);

            return item;
        }
        
        public Task RestSendBigAsync(CreateBigCommand command)
        {
            var json = JsonSerializer.Serialize(command);
            var payload = new StringContent(
                json, 
                System.Text.Encoding.UTF8, 
                "application/json"
            );
            
            return HttpClient.PostAsync("big", payload);
        }
        
        public Task RestSendBigEventAsync(CreateBigEventsCommand command)
        {
            var json = JsonSerializer.Serialize(command);
            var payload = new StringContent(
                json, 
                System.Text.Encoding.UTF8, 
                "application/json"
            );
            
            return HttpClient.PostAsync("big/events", payload);
        }
    }
}