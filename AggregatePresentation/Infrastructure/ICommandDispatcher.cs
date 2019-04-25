using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AggregatePresentation.Infrastructure
{
    public interface ICommandDispatcher
    {
        Task Execute(ICommand command, CancellationToken cancellationToken);
        Task<TResult> Execute<TResult>(ICommandWithResult<TResult> command, CancellationToken cancellationToken);
    }
}
