using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AggregatePresentation.Aggregates;
using AggregatePresentation.Services.FullNameService;
using AggregatePresentation.Services.IdGenerator;

namespace AggregatePresentation.Processors
{
    public class CreateSomethingAggregateProcessor : ICreateSomethingAggregateProcessor
    {
        private readonly IFullNameService fullNameService;
        private readonly IUniqueIdGenerator idGenerator;

        public CreateSomethingAggregateProcessor(IFullNameService fullNameService, IUniqueIdGenerator idGenerator)
        {
            this.fullNameService = fullNameService;
            this.idGenerator = idGenerator;
        }

        public async Task<SomethingAggregate> CreateSomething(string firstName, string lastName, CancellationToken cancellationToken)
        {
            var aggregate = await SomethingAggregate.CreateBest(idGenerator,
                                                                fullNameService,
                                                                firstName,
                                                                lastName);
            return aggregate;
        }
    }
}
