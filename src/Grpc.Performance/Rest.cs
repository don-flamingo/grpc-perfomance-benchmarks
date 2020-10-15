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
    public class Rest
    {
        private readonly HttpClient _httpClient;
        public Rest()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:6000");
        }
            
        [Benchmark]
        public async Task GetBigDtoAsync()
        {
            var response = await _httpClient.GetAsync("big-dto");
            var json = await response.Content.ReadAsStringAsync();
            var item = JsonSerializer.Deserialize<ICollection<BigDto>>(json);
        }
        
    }
}