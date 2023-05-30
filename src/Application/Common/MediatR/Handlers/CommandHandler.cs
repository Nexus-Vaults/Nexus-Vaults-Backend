using Nexus.Application.Common.MediatR;
using Nexus.Application.Services;

namespace Nexus.Application.Common;
public abstract class CommandHandler<TRequest, TResponse> : FarsightRequestHandler<TRequest, TResponse>
    where TRequest : Command<TResponse>
{
}