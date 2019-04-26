using AggregatePresentation.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AggregatePresentation.PipelineBehavior
{
    public class LoggingPipelineBehavior<TCommand, TResponse> : IPipelineBehavior<TCommand, TResponse>
        where TCommand : ICommandWithResult<TResponse>
    {
        public async Task<TResponse> Handle(TCommand request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var logString = string.Format("Command: {0} is in the pipeline.", request.GetType());
            Console.WriteLine(logString);

            return await next();
        }
    }
}
