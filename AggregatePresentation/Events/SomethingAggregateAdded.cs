using CQRSlite.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AggregatePresentation.Events
{
    public class SomethingAggregateAdded : AbstractEvent
    {
        public SomethingAggregateAdded(string firstName, string lastName, string fullName)
        {
            FirstName = firstName;
            LastName = lastName;
            FullName = fullName;
        }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string FullName { get; private set; }
    }
}
