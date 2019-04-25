using AggregatePresentation.Commands;
using AggregatePresentation.Services.FullNameService;
using AggregatePresentation.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AggregatePresentation.Services.IdGenerator;
using AggregatePresentation.Aggregates;
using CQRSlite.Domain;
using AggregatePresentation.Processors;

namespace AggregatePresentation.CommandHandlers
{
    public class DoSomethingBonusCommandHandler : ICommandHandler<DoSomethingCommand, Guid>
    {
        private readonly ICreateSomethingAggregateProcessor processor;
        private readonly ISession session;

        public DoSomethingBonusCommandHandler(ICreateSomethingAggregateProcessor processor,
                                              ISession session)
        {
            this.processor = processor;
            this.session = session;
        }

        public async Task<Guid> Handle(DoSomethingCommand request, CancellationToken cancellationToken)
        {
            var aggregate = await processor.CreateSomething(request.FirstName, request.LastName, cancellationToken);
            await session.Add(aggregate);
            await session.Commit();
            return aggregate.Id;
        }
    }
}
