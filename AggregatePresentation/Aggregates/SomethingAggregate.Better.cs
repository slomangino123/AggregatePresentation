using AggregatePresentation.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AggregatePresentation.Aggregates
{
    public partial class SomethingAggregate
    {
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
    }
}
