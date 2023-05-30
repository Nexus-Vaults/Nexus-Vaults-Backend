using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Nexus.Application.Handlers;

public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TRequest> Logger;

    public PerformanceBehaviour(
        ILogger<TRequest> logger)
    {
        Logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        long startTime = Stopwatch.GetTimestamp();

        var response = await next();

        long elapsedMilliseconds = Stopwatch.GetElapsedTime(startTime).Milliseconds;

        if (elapsedMilliseconds > 500)
        {
            string? requestName = typeof(TRequest).Name;

            Logger.LogWarning("CleanArchitecture Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@Request}",
                requestName, elapsedMilliseconds, request);
        }

        return response;
    }
}