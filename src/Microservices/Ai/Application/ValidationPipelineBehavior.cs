using Domain.Shared;
using FluentValidation;
using MediatR;

namespace Application
{
    public class ValidationPipelineBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Result
    {
        private readonly IEnumerable<IValidator<TRequest>> validators;

        public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators) {
            this.validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken) {
            if (!validators.Any()) {
                return await next();
            }

            var validationResults = await Task.WhenAll(validators
                .Select(validator => validator.ValidateAsync(request)));

            Error[] errors = validationResults
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .Select(error => new Error(error.PropertyName, error.ErrorMessage))
                .Distinct()
                .ToArray();

            if (errors.Any()) {
                return CreateValidationResult<TResponse>(errors);
            }

            return await next();
        }

        private static TResult CreateValidationResult<TResult>(Error[] errors)
            where TResult : Result {
            if (typeof(TResult) == typeof(Result)) {
                return (ValidationResult.WithErrors(errors) as TResult)!;
            }
            object validationResult = typeof(ValidationResult<>)
                .GetGenericTypeDefinition()
                .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
                .GetMethod(nameof(ValidationResult.WithErrors))!
                .Invoke(null, new object[] { errors })!;
            return (TResult)validationResult;
        }
    }
}
