using MediatR;

namespace DDDTest.Domain.Framework.Messaging;
public interface IQuery<out TResponse> : IRequest<TResponse> {
}
;
