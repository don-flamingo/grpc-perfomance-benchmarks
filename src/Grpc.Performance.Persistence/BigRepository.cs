using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Grpc.Perfomance.Contracts.Big;
using Grpc.Perfomance.Contracts.Medium;

namespace Grpc.Performance.Application
{
    public interface IBigRepository
    {
        IEnumerable<BigDto> Get();
        Task<ICollection<BigDto>> GetAsync();
    }
    
    public class BigRepository : IBigRepository
    {
        private readonly ICollection<BigDto> _database;
        
        public BigRepository()
        {
            _database = new Faker<BigDto>()
                .RuleFor(p => p.Id, Guid.NewGuid())
                .RuleFor(p => p.Text, f => f.Lorem.Text())
                .RuleFor(p => p.Content, f => f.Lorem.Paragraphs())
                .RuleFor(p => p.Decimal, f => f.Finance.Amount())
                .RuleFor(p => p.Long, f => f.Random.Long())
                .RuleFor(p => p.Double, f => f.Random.Double())
                .RuleFor(p => p.Float, f => f.Random.Float())
                .RuleFor(p => p.Number, f => f.Random.Int())
                .RuleFor(p => p.DateTime, f => f.Person.DateOfBirth)
                .RuleFor(p => p.Medium, f => new MediumDto
                {
                    Id = f.Random.Guid(),
                    Text = f.Random.String(),
                    Paragraph = f.Lorem.Paragraph(),
                    Bytes = f.Random.Bytes(256)
                })
                .Generate(500);
        }

        public IEnumerable<BigDto> Get()
            => _database;

        public Task<ICollection<BigDto>> GetAsync()
            => Task.FromResult(_database);
    }
}