using MediatR;
using Nexus.Application.Common.MediatR;

namespace Nexus.Application.Common;
public abstract class Query<T> : IRequest<MediatrResult>
{
}
