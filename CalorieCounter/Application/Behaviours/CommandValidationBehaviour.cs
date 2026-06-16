using ErrorOr;
using FluentValidation;
using MediatR;

namespace CalorieCounter.Application.Behaviours
{
    public class CommandValidationBehaviour<TRequest, TResponse>(IEnumerable<IValidator<TRequest>>? validators = null)
        : IPipelineBehavior<TRequest, ErrorOr<TResponse>>
        where TRequest : IRequest<ErrorOr<TResponse>>
    {
        private readonly IEnumerable<IValidator<TRequest>>? _validators = validators;

        public async Task<ErrorOr<TResponse>> Handle(TRequest request, RequestHandlerDelegate<ErrorOr<TResponse>> next, CancellationToken cancellationToken)
        {
            if (_validators is null)
                return await next(cancellationToken);

            ValidationContext<TRequest> context = new(request);

            FluentValidation.Results.ValidationResult[] validationResults = await Task.WhenAll(
                _validators.Select(validator => validator.ValidateAsync(context, cancellationToken)));

            List<FluentValidation.Results.ValidationFailure> errors = validationResults
                .SelectMany(result => result.Errors)
                .ToList();

            if (errors.Count > 0)
            {
                return errors
                   .ConvertAll(error => Error.Validation(
                       code: error.PropertyName,
                       description: error.ErrorMessage));
            }

            return await next(cancellationToken);
        }
    }
}
