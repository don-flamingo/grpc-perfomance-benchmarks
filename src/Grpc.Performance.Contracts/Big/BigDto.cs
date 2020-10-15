using System;
using System.Runtime.Serialization;
using Grpc.Perfomance.Contracts.Medium;

namespace Grpc.Perfomance.Contracts.Big
{
    [DataContract]
    public class BigDto
    {
        [DataMember(Order = 1)]
        public Guid Id { get; set; }
        [DataMember(Order = 2)]
        public string Text { get; set; }
        [DataMember(Order = 3)]
        public DateTime DateTime { get; set; }
        [DataMember(Order = 4)]
        public int Number { get; set; }
        [DataMember(Order = 5)]
        public float Float { get; set; }
        [DataMember(Order = 6)]
        public decimal Decimal { get; set; }
        [DataMember(Order = 7)]
        public double Double { get; set; }
        [DataMember(Order = 8)]
        public long Long { get; set; } 
        [DataMember(Order = 9)]
        public MediumDto Medium { get; set; }
        [DataMember(Order = 10)]
        public string Content { get; set; }
    }
}