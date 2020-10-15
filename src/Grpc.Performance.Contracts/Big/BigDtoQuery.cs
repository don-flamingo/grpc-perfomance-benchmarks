using System.Runtime.Serialization;

namespace Grpc.Perfomance.Contracts.Big
{
    [DataContract]
    public class BigDtoQuery
    {
        [DataMember(Order = 1)]
        public string Where { get; set; }
    }
}