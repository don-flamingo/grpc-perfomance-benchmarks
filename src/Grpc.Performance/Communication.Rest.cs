using System;
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
        public async Task RestGetCollectionBigDtoAsync()
        {
            var response = await _httpClient.GetAsync("big");
            var json = await response.Content.ReadAsStringAsync();
            var item = JsonSerializer.Deserialize<ICollection<BigDto>>(json);
        }
        
        [Benchmark]
        public async Task RestGetCollectionBigDtoWithoutClientAsync()
        {
            var client = CreateClient();
            var response = await client.GetAsync("big");
            var json = await response.Content.ReadAsStringAsync();
            var item = JsonSerializer.Deserialize<ICollection<BigDto>>(json);
        }
        
        [Benchmark]
        public async Task RestGetBigDtoAsync()
        {
            var response = await _httpClient.GetAsync("big/item");
            var json = await response.Content.ReadAsStringAsync();
            var item = JsonSerializer.Deserialize<BigDto>(json);
        }
        
        [Benchmark]
        public async Task RestGetBigDtoWithoutClientAsync()
        {
            var client = CreateClient();
            var response = await _httpClient.GetAsync("big/item");
            var json = await response.Content.ReadAsStringAsync();
            var item = JsonSerializer.Deserialize<BigDto>(json);
        }
        
        private HttpClient CreateClient()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => true
            };

            return new HttpClient(handler)
            {
                BaseAddress = new Uri("http://localhost:6000/")
            };
        }
    }
}