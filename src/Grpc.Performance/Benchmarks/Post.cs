using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Grpc.Performance.Fixtures;

namespace Grpc.Performance.Benchmarks
{
    public class Post
    {
        private readonly InfrastructureFixture _infrastructure;
        private readonly CommandsFixture _commands;
        
        [Params(1, 10, 20)]
        public int Count { get; set; }
        
        public Post()
        {
            _infrastructure = new InfrastructureFixture();
            _commands = new CommandsFixture();
        }

        [Benchmark]
        [BenchmarkCategory(Categories.Grpc)]
        public async Task GrpcSendBigCommand()
        {
            for (var i = 0; i < Count; i++)
            {
                await _infrastructure.CodeFirstService.SendBigCommandAsync(
                    _commands.CreateBigCommand,
                    CancellationToken.None);
            }
        }
        
        [Benchmark]
        [BenchmarkCategory(Categories.Grpc)]
        public async Task GrpcSendBigEventCommand()
        {
            for (var i = 0; i < Count; i++)
            {
                await _infrastructure.CodeFirstService.SendBigEventsCommandAsync(
                    _commands.CreateBigEventsCommand,
                    CancellationToken.None);
            }
        }
        
        [Benchmark]
        [BenchmarkCategory(Categories.Rest)]
        public async Task RestSendBigCommand()
        {
            for (var i = 0; i < Count; i++)
            {
                await _infrastructure.RestSendBigAsync(
                    _commands.CreateBigCommand);
            }
        }
        
        [Benchmark]
        [BenchmarkCategory(Categories.Rest)]
        public async Task RestSendBigEventCommand()
        {
            for (var i = 0; i < Count; i++)
            {
                await _infrastructure.RestSendBigEventAsync(
                    _commands.CreateBigEventsCommand);
            }
        }
    }
}