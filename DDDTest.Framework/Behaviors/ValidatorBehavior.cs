using DDDTest.Domain.Framework.Exceptions;
using DDDTest.Domain.Framework.Messaging;
using FluentValidation;
using MediatR;

namespace DDDTest.Domain.Framework.Behaviors;
//https://code-maze.com/cqrs-mediatr-fluentvalidation/
public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, ICommand<TResponse> {
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next) {

        if (!_validators.Any()) {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);
       
        //var errorsDictionary = _validators
        //    .Select(x => x.Validate(context))
        //    .SelectMany(x => x.Errors)
        //    .Where(x => x != null)
        //    .GroupBy(
        //        x => x.PropertyName,
        //        x => x.ErrorMessage,
        //        (propertyName, errorMessages) => new {
        //            Key = propertyName,
        //            Values = errorMessages.Distinct().ToArray()
        //        })
        //    .ToDictionary(x => x.Key, x => x.Values);
        var failures = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .Distinct()
                .ToList();

        if (failures.Any()) {
            var message = string.Join(" | ", failures);
            throw new AppException(ApiResultStatusCode.BadRequest, message);
        }

        return await next();
    }
}


