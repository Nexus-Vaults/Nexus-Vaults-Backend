using Nexus.Application.Common.MediatR;

namespace Nexus.Application.Common;
public abstract class CommandHandler<TRequest, TResponse> : FarsightRequestHandler<TRequest, TResponse>
    where TRequest : Command<TResponse>
{
}