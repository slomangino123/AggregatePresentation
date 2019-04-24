using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AggregatePresentation.Services.IdGenerator
{
    public interface IUniqueIdGenerator
    {
        Guid GenerateGuid();
    }
}
