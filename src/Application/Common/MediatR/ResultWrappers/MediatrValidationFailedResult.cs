using FluentValidation.Results;

namespace Nexus.Application.Common.MediatR;
public class MediatrValidationFailedResult : MediatrResult
{
    public IDictionary<string, string[]> Errors { get; }

    public MediatrValidationFailedResult(List<ValidationFailure> validationFailures)
        : base(ResultStatus.ValidationFailed)
    {
        Errors = validationFailures
         .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
         .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }
}
