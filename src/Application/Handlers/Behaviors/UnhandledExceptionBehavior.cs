using MediatR;
using Microsoft.Extensions.Logging;

namespace Nexus.Application.Handlers;

public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TRequest> Logger;

    public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
    {
        Logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            string? requestName = typeof(TRequest).Name;

            Logger.LogError(ex, "Farsight Request: Unhandled Exception for Request {Name} {@Request}", requestName, request);

            throw;
        }
    }
}