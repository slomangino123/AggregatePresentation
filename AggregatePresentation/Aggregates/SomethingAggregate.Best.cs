using AggregatePresentation.Events;
using AggregatePresentation.Services.FullNameService;
using AggregatePresentation.Services.IdGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AggregatePresentation.Aggregates
{
    public partial class SomethingAggregate
    {

        public static async Task<SomethingAggregate> CreateBest(IUniqueIdGenerator idGenerator,
                                                                IFullNameService fullNameService,
                                                                string firstName,
                                                                string lastName)
        {
            ThrowIfFirstNameIsEmpty(firstName);
            ThrowIfLastNameIsEmpty(lastName);

            var id = idGenerator.GenerateGuid();
            var agg = new SomethingAggregate(id);

            var fullName = await fullNameService.BuildFullName(firstName, lastName);

            agg.ApplyChange(new SomethingAggregateAdded(firstName, lastName, fullName));
            return agg;
        }

        private static void ThrowIfFirstNameIsEmpty(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new Exception("First Name is empty.");
            }
        }

        private static void ThrowIfLastNameIsEmpty(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new Exception("Last Name is empty.");
            }
        }
    }
}
