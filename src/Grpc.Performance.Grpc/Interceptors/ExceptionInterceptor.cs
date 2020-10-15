using System;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Logging;

namespace Grpc.Perfomance.Grpc.Interceptors
{
    public class ExceptionInterceptor : Interceptor
    {
        private ILogger<ExceptionInterceptor> _logger;

        public ExceptionInterceptor(ILogger<ExceptionInterceptor> logger)
        {
            _logger = logger;
        }

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                return await continuation(request, context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, context.Method);
                
                throw new RpcException(new Status(StatusCode.Internal, exception.StackTrace));
            }
        }
    }
}