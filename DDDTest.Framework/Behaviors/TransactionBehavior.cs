using DDDTest.Domain.Framework.Messaging;
using MediatR;
using System.Transactions;

namespace DDDTest.Domain.Framework.Behaviors;
public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
where TRequest : class, ICommand<TResponse> {
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next) {
        var transactionOptions = new TransactionOptions {
            IsolationLevel = IsolationLevel.ReadCommitted,
            Timeout = TransactionManager.MaximumTimeout
        };

        using (var transaction = new TransactionScope(TransactionScopeOption.Required, transactionOptions,
            TransactionScopeAsyncFlowOption.Enabled)) {
            TResponse response = await next();
            transaction.Complete();

            return response;
        }
    }
}

