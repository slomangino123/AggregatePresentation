using AggregatePresentation.Commands;
using AggregatePresentation.Events;
using AggregatePresentation.Services.FullNameService;
using AggregatePresentation.Services.IdGenerator;
using CQRSlite.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AggregatePresentation.Aggregates
{
    public class SomethingAggregate : AggregateRoot
    {
        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        private SomethingAggregate(Guid id)
        {
            Id = id;
        }

        public static SomethingAggregate Create(Guid id,
                                                DoSomethingCommand command,
                                                string fullName)
        {
            var agg = new SomethingAggregate(id);
            agg.ApplyChange(new SomethingAggregateAdded(command.FirstName, command.LastName, fullName));
            return agg;
        }

        public static SomethingAggregate CreateBetter(Guid id,
                                                      string firstName,
                                                      string lastName, 
                                                      string fullName)
        {
            var agg = new SomethingAggregate(id);

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                throw new Exception("First Name and/or Last Name are empty.");
            }

            agg.ApplyChange(new SomethingAggregateAdded(firstName, lastName, fullName));
            return agg;
        }

        public static async Task<SomethingAggregate> CreateBest(IUniqueIdGenerator idGenerator,
                                                                IFullNameService fullNameService,
                                                                string firstName,
                                                                string lastName)
        {
            ThrowIfFirstNameOrLastNameAreEmpty(firstName, lastName);

            var id = idGenerator.GenerateGuid();
            var agg = new SomethingAggregate(id);

            var fullName = await fullNameService.BuildFullName(firstName, lastName);

            agg.ApplyChange(new SomethingAggregateAdded(firstName, lastName, fullName));
            return agg;
        }

        public void Apply(SomethingAggregateAdded @event)
        {
            FirstName = @event.FirstName;
            LastName = @event.LastName;
        }

        private static void ThrowIfFirstNameOrLastNameAreEmpty(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                throw new Exception("First Name and/or Last Name are empty.");
            }
        }
    }
}
