using AggregatePresentation.Aggregates;
using AggregatePresentation.Commands;
using AggregatePresentation.Infrastructure;
using AggregatePresentation.Services.FullNameService;
using CQRSlite.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AggregatePresentation.CommandHandlers
{
    public class ChangeFirstNameCommandHandler : AbstractCommandHandler<ChangeFirstNameCommand>
    {
        private readonly IFullNameService fullNameService;
        private readonly ISession session;

        public ChangeFirstNameCommandHandler(IFullNameService fullNameService,
                                             ISession session)
        {
            this.fullNameService = fullNameService;
            this.session = session;
        }

        protected override async Task Handle(ChangeFirstNameCommand request, CancellationToken cancellationToken)
        {
            var aggregate = await session.Get<SomethingAggregate>(request.AggregateId,
                                                                  cancellationToken: cancellationToken);
            await aggregate.ChangeFirstName(request.FirstName, fullNameService);

            await session.Commit();
        }
    }
}
