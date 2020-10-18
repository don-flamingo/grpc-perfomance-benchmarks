using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Performance.Contracts.Big;

namespace Grpc.Performance.Contracts.Big
{
    [ServiceContract]
    public interface IBigService
    {
        Task<BigDto> GetBigDtoAsync(CancellationToken cancellationToken);
        Task SendBigCommandAsync(CreateBigCommand command, CancellationToken cancellationToken);
        Task SendBigEventsCommandAsync(CreateBigEventsCommand command, CancellationToken cancellationToken);
        Task<ICollection<BigDto>> GetBigDtosAsync(CancellationToken cancellationToken);
        IAsyncEnumerable<BigDto> GetBigDtosStreamAsync(BigDtoQuery query, CancellationToken cancellationToken);
        Task ThrowExceptionAsync(CancellationToken cancellationToken);
    }
}