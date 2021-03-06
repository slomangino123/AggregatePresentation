﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AggregatePresentation.Infrastructure
{
    public interface ICommand : IRequest, ICommandWithResult<Unit>
    {
    }

    public interface ICommandWithResult<TResult> : IRequest<TResult>
    {
    }
}
