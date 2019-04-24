using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AggregatePresentation.Services.FullNameService
{
    public interface IFullNameService
    {
        Task<string> BuildFullName(string firstName, string lastName);
    }
}
