using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Nexus.Application.Handlers;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger Logger;

    public LoggingBehaviour(ILogger<TRequest> logger)
    {
        Logger = logger;
    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        string? requestName = typeof(TRequest).Name;

        Logger.LogDebug("Processing Request: {name} {request}",
            requestName, request);
        return Task.CompletedTask;
    }
}