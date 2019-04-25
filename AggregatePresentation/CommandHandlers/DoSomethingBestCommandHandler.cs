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

namespace AggregatePresentation.CommandHandlers
{
    public class DoSomethingBestCommandHandler : ICommandHandler<DoSomethingCommand, Guid>
    {
        private readonly IFullNameService fullNameService;
        private readonly IUniqueIdGenerator idGenerator;
        private readonly ISession session;

        public DoSomethingBestCommandHandler(IFullNameService fullNameService,
                                             IUniqueIdGenerator idGenerator,
                                             ISession session)
        {
            this.fullNameService = fullNameService;
            this.idGenerator = idGenerator;
            this.session = session;
        }

        public async Task<Guid> Handle(DoSomethingCommand request, CancellationToken cancellationToken)
        {
            var aggregate = await SomethingAggregate.CreateBest(idGenerator,
                                                                fullNameService,
                                                                request.FirstName,
                                                                request.LastName);
            await session.Add(aggregate);
            await session.Commit();
            return aggregate.Id;
        }
    }
}
