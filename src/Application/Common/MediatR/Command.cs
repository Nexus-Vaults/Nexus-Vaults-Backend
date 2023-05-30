using MediatR;
using Nexus.Application.Common.MediatR;

namespace Nexus.Application.Common;
public abstract class Command<T> : IRequest<MediatrResult>
{
}
