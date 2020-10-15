using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Grpc.Perfomance.Contracts.Medium
{
    [DataContract]
    public class MediumDto
    {
        [DataMember(Order = 1)]
        public Guid Id { get; set; }
        [DataMember(Order = 2)]
        public string Text { get; set; }
        [DataMember(Order = 3)]
        public ICollection<byte> Bytes { get; set; } 
        [DataMember(Order = 4)]
        public string Paragraph { get; set; }
    }
}