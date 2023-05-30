using MediatR;

namespace Nexus.Application.Common.MediatR;
public abstract class FarsightRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, MediatrResult>
    where TRequest : IRequest<MediatrResult>
{
    protected CancellationToken CancellationToken;
    protected bool IsForbidden = false;

    async Task<MediatrResult> IRequestHandler<TRequest, MediatrResult>.Handle(TRequest request, CancellationToken cancellationToken)
    {
        CancellationToken = cancellationToken;

        var result = await HandleAsync(request, cancellationToken);

        return IsForbidden
            ? FarsightRequestHandler<TRequest, TResponse>.Forbidden()
            : new MediatrValueResult<TResponse>(result);
    }

    public abstract Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);

    private static MediatrResult Forbidden()
    {
        return new MediatrResult(ResultStatus.Forbidden);
    }
}
