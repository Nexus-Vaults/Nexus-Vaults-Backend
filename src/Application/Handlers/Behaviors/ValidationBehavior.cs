using FluentValidation;
using MediatR;
using Nexus.Application.Common.MediatR;

namespace Nexus.Application.Handlers;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, MediatrResult>
    where TRequest : IRequest<TResponse>
    where TResponse : MediatrResult
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<MediatrResult> Handle(TRequest request, RequestHandlerDelegate<MediatrResult> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(
                _validators.Select(v =>
                    v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .Where(r => r.Errors.Any())
                .SelectMany(r => r.Errors)
                .ToList();

            if (failures is not null && failures.Any())
            {
                return new MediatrValidationFailedResult(failures);
            }
        }
        return await next();
    }
}