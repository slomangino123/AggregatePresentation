using AggregatePresentation.Events;
using AggregatePresentation.Services.FullNameService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AggregatePresentation.Aggregates
{
    public partial class SomethingAggregate
    {
        public async Task ChangeFirstName(string firstName,
                                           IFullNameService fullNameService)
        {
            ThrowIfFirstNameIsEmpty(firstName);

            var newFullName = await fullNameService.BuildFullName(firstName, LastName);

            ApplyChange(new FirstNameChanged(firstName, newFullName));
        }

        public void Apply(FirstNameChanged @event)
        {
            FirstName = @event.FirstName;
        }
    }
}
