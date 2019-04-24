using AggregatePresentation.Commands;
using AggregatePresentation.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AggregatePresentation.Mapping
{
    public static class SomethingMappingExtensions
    {
        public static DoSomethingCommand ToDoSomethingCommand(this InputDTO inputDTO)
        {
            return new DoSomethingCommand(inputDTO.FirstName, inputDTO.LastName);
        }
    }
}
