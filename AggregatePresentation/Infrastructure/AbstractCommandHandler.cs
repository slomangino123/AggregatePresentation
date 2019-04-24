using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AggregatePresentation.Infrastructure
{
    public abstract class AbstractCommandHandler<TCommand> : AsyncRequestHandler<TCommand>
        where TCommand : ICommand
    {
    }
}
