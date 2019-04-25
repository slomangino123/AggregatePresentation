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
    public class DoSomethingBetterCommandHandler : ICommandHandler<DoSomethingCommand, Guid>
    {
        private readonly IFullNameService fullNameService;
        private readonly IUniqueIdGenerator idGenerator;
        private readonly ISession session;

        public DoSomethingBetterCommandHandler(IFullNameService fullNameService,
                                               IUniqueIdGenerator idGenerator,
                                               ISession session)
        {
            this.fullNameService = fullNameService;
            this.idGenerator = idGenerator;
            this.session = session;
        }

        public async Task<Guid> Handle(DoSomethingCommand request, CancellationToken cancellationToken)
        {
            var newId = idGenerator.GenerateGuid();
            var fullName = await fullNameService.BuildFullName(request.FirstName, request.LastName);

            var aggregate = SomethingAggregate.CreateBetter(newId,
                                                            request.FirstName,
                                                            request.LastName,
                                                            fullName);
            await session.Add(aggregate);
            await session.Commit();
            return aggregate.Id;
        }
    }
}
