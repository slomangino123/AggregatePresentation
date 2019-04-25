using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AggregatePresentation.Events
{
    public class FirstNameChanged : AbstractEvent
    {
        public FirstNameChanged(string firstName, string fullName)
        {
            FirstName = firstName;
            FullName = fullName;
        }

        public string FirstName { get; private set; }
        public string FullName { get; private set; }
    }
}
