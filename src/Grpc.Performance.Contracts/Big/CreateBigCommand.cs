using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Grpc.Performance.Contracts.Big
{
    [DataContract]
    public class CreateBigCommand
    {
        [DataMember(Order = 1)]
        public Guid Id { get; set; }
        [DataMember(Order = 2)]
        public string Text { get; set; }
        [DataMember(Order = 3)]
        public DateTime DateTime { get; set; }
    }
    
    [DataContract]
    public class CreateBigEventsCommand
    {
        [DataMember(Order = 1)]
        public Guid Id { get; set; }
        [DataMember(Order = 2)]
        public string Text { get; set; }
        [DataMember(Order = 3)]
        public DateTime DateTime { get; set; }
        
        [DataMember(Order = 4)]
        public ICollection<CreateBigEventsCommand> Events { get; set; }
    }
}