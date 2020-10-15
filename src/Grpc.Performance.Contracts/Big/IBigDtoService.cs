using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Perfomance.Contracts.Big;

namespace Grpc.Performance.Contracts.Big
{
    [ServiceContract]
    public interface IBigDtoService
    {
        ValueTask<BigDto> GetBigDtoAsync(CancellationToken cancellationToken);
        ValueTask<ICollection<BigDto>> GetBigDtosAsync(CancellationToken cancellationToken);
        IAsyncEnumerable<BigDto> GetBigDtosStreamAsync(BigDtoQuery query, CancellationToken cancellationToken);
        ValueTask ThrowExceptionAsync(CancellationToken cancellationToken);
    }
}