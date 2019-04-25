using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AggregatePresentation.DTOs;
using AggregatePresentation.Infrastructure;
using AggregatePresentation.Mapping;
using Microsoft.AspNetCore.Mvc;

namespace AggregatePresentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ICommandDispatcher commandDispatcher;

        public ValuesController(ICommandDispatcher commandDispatcher)
        {
            this.commandDispatcher = commandDispatcher;
        }

        [HttpPost("DoSomething")]
        public async Task<ActionResult<Guid>> DoSomething([FromBody] InputDTO inputDTO, CancellationToken cancellationToken)
        {
            var command = inputDTO.ToDoSomethingCommand();
            var result = await commandDispatcher.Execute(command, cancellationToken);
            return Ok(result);
        }

        [HttpPost("changefirstname")]
        public async Task<ActionResult> ChangeFirstName([FromBody] ChangeFirstNameInputDTO inputDTO, CancellationToken cancellationToken)
        {
            var command = inputDTO.ToChangeFirstNameCommand();
            await commandDispatcher.Execute(command, cancellationToken);
            return Ok();
        }
    }
}
