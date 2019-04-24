using AggregatePresentation.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AggregatePresentation.Processors
{
    public interface ICreateSomethingAggregateProcessor
    {
        Task<SomethingAggregate> CreateSomething(string firstName, string lastName, CancellationToken cancellationToken);
    }
}
