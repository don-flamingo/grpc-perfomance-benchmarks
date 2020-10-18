using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bogus;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Performance.Grpc.Proto;

namespace Grpc.Performance.Application
{
    public interface IBigEntityDtoRepository
    {
        IEnumerable<BigDto> Get();
        Task<ICollection<BigDto>> GetAsync();
    }
    
    public class BigEntityDtoRepository : IBigEntityDtoRepository
    {
        private readonly ICollection<BigDto> _database;
        
        public BigEntityDtoRepository()
        {
            _database = new Faker<BigDto>()
                .RuleFor(p => p.Id, Guid.NewGuid().ToString())
                .RuleFor(p => p.Text, f => f.Lorem.Text())
                .RuleFor(p => p.Content, f => f.Lorem.Paragraphs())
                .RuleFor(p => p.Decimal,  f => (float) f.Finance.Amount())
                .RuleFor(p => p.Long, f => f.Random.Long())
                .RuleFor(p => p.Double, f => f.Random.Double())
                .RuleFor(p => p.Float, f => f.Random.Float())
                .RuleFor(p => p.Number, f => f.Random.Int())
                .RuleFor(p => p.DateTime, f => Timestamp.FromDateTime(f.Person.DateOfBirth.ToUniversalTime()))
                .RuleFor(p => p.Medium, f => new MediumDto
                {
                    Id = f.Random.Guid().ToString(),
                    Text = f.Random.String(),
                    Paragraph = f.Lorem.Paragraph(),
                    Bytes = ByteString.CopyFrom( f.Random.Bytes(256))
                })
                .Generate(500);
        }

        public IEnumerable<BigDto> Get()
            => _database;

        public Task<ICollection<BigDto>> GetAsync()
            => Task.FromResult(_database);
    }
}