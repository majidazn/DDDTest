using MediatR;

namespace DDDTest.Domain.Framework.Messaging;
public interface ICommand<out TResponse> : IRequest<TResponse> {
}

