using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AggregatePresentation.DTOs
{
    public class ChangeFirstNameInputDTO
    {
        public ChangeFirstNameInputDTO(Guid aggregateId, string firstName)
        {
            AggregateId = aggregateId;
            FirstName = firstName;
        }

        public Guid AggregateId { get; private set; }
        public string FirstName { get; private set; }
    }
}
