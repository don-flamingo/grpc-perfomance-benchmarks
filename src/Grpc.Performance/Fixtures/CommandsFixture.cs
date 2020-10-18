using System;
using Bogus;
using Grpc.Performance.Contracts.Big;

namespace Grpc.Performance.Fixtures
{
    public class CommandsFixture
    {
        public CreateBigCommand CreateBigCommand { get; }
        public CreateBigEventsCommand CreateBigEventsCommand { get; }

        public CommandsFixture()
        {
            CreateBigCommand = new Faker<CreateBigCommand>()
                .RuleFor(p => p.Id, Guid.NewGuid)
                .RuleFor(p => p.Text, f => f.Lorem.Paragraphs(20))
                .RuleFor(p => p.DateTime, DateTime.UtcNow);

            CreateBigEventsCommand = new Faker<CreateBigEventsCommand>()
                .RuleFor(p => p.Id, Guid.NewGuid)
                .RuleFor(p => p.Text, f => f.Lorem.Paragraphs(20))
                .RuleFor(p => p.DateTime, DateTime.UtcNow)
                .RuleFor(p => p.Events, f =>
                {
                    return new Faker<CreateBigEventsCommand>()
                        .RuleFor(p => p.Id, Guid.NewGuid)
                        .RuleFor(p => p.Text, f => f.Lorem.Paragraphs(20))
                        .RuleFor(p => p.DateTime, DateTime.UtcNow)
                        .Generate(20);
                });
        }
    }
}