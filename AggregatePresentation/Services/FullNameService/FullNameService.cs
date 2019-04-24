using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AggregatePresentation.Services.FullNameService
{
    public class FullNameService : IFullNameService
    {
        public Task<string> BuildFullName(string firstName, string lastName)
        {
            return Task.FromResult(firstName + " " + lastName);
        }
    }
}
