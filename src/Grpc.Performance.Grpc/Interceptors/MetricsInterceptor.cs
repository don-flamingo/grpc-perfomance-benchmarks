using System;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Logging;

namespace Grpc.Perfomance.Grpc.Interceptors
{
    public class MetricsInterceptor : Interceptor
    {
        private ILogger<MetricsInterceptor> _logger;

        public MetricsInterceptor(ILogger<MetricsInterceptor> logger)
        {
            _logger = logger;
        }

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            // Metric
            
            return await continuation(request, context);
        }
    }
}