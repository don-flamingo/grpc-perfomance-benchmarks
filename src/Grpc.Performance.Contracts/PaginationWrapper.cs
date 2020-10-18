using System.Collections.Generic;
using System.Runtime.Serialization;
using Grpc.Performance.Contracts.Big;

namespace Grpc.Perfomance.Contracts
{
    [DataContract]
    public class PaginationWrapper<TModel>
    {
        [DataMember(Order = 1)]
        public ICollection<TModel> Items { get; set; } = new List<TModel>();
        [DataMember(Order = 2)]
        public int TotalSize { get; set; }
    }
}