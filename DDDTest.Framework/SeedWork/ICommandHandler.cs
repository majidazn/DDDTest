using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDDTest.Domain.Framework.Messaging;
using MediatR;

namespace DDDTest.Domain.Framework.SeedWork {
    public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
          where TCommand : ICommand<TResponse> {
    }
}
