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
    public partial class SomethingAggregate : AggregateRoot
    {
        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        private SomethingAggregate() { }

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

        public void Apply(SomethingAggregateAdded @event)
        {
            FirstName = @event.FirstName;
            LastName = @event.LastName;
        }
    }
}
