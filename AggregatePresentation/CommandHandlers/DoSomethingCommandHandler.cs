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
#pragma warning disable CS4014

namespace AggregatePresentation.CommandHandlers
{
    public class DoSomethingCommandHandler : AbstractCommandHandler<DoSomethingCommand>
    {
        private readonly IFullNameService fullNameService;
        private readonly IUniqueIdGenerator idGenerator;
        private readonly ISession session;

        public DoSomethingCommandHandler(IFullNameService fullNameService,
                                         IUniqueIdGenerator idGenerator,
                                         ISession session)
        {
            this.fullNameService = fullNameService;
            this.idGenerator = idGenerator;
            this.session = session;
        }

        protected override async Task Handle(DoSomethingCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.FirstName) || string.IsNullOrWhiteSpace(request.LastName))
            {
                throw new Exception("First Name and/or Last Name are empty.");
            }

            var newId = idGenerator.GenerateGuid();
            var fullName = await fullNameService.BuildFullName(request.FirstName, request.LastName);

            var aggregate = SomethingAggregate.Create(newId,
                                                      request,
                                                      fullName);

            session.Add(aggregate);
            session.Commit();
        }
    }
}

#pragma warning restore CS4014
