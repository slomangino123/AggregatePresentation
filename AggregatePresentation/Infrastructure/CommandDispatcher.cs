using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AggregatePresentation.Infrastructure
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IMediator mediator;

        public CommandDispatcher(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task Execute(ICommand command, CancellationToken cancellationToken)
        {
            await mediator.Send(command);
        }

        public async Task<TResult> Execute<TResult>(ICommandWithResult<TResult> command, CancellationToken cancellationToken)
        {
            return await mediator.Send(command);
        }
    }
}
