using System;
using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Grpc.Net.Client;
using Grpc.Performance.Contracts.Big;
using ProtoBuf.Grpc.Client;


namespace Grpc.Performance
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            BenchmarkRunner.Run<Grpc>();
            BenchmarkRunner.Run<Rest>();
        }
    }
}