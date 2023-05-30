namespace Nexus.Application.Common.MediatR;
public class MediatrValueResult<T> : MediatrResult
{
    public T Value { get; }

    public MediatrValueResult(T value)
        : base(ResultStatus.Value)
    {
        Value = value;
    }
}
