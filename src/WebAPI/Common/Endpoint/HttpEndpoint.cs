using AutoMapper;
using Common.Services;
using MediatR;
using Nexus.Application.Common.MediatR;
using Nexus.Contracts;
using Nexus.WebAPI.Common.Attributes;
using Nexus.WebAPI.Common.Exceptions;

namespace Nexus.WebAPI.Common;

public abstract class HttpEndpoint<TContract, TRequest, TResult, TResponse> : Singleton, IHttpEndpoint<TContract>
    where TContract : IRequestContract
    where TRequest : IRequest<MediatrResult>
    where TResult : class
    where TResponse : IResponseContract
{
    [Inject]
    private readonly IMapper Mapper = null!;

    public virtual void ConfigureContractToRequestMapping(Profile profile)
    {
        profile.CreateMap<TContract, TRequest>();
    }

    public virtual void ConfigureResultToResponseMapping(Profile profile)
    {
        profile.CreateMap<TResult, TResponse>();
    }

    public abstract IResult MapToResponse(TResult result);

    public TResponse ResultToContract(TResult result)
    {
        return Mapper.Map<TResult, TResponse>(result);
    }

    public IEndpointConventionBuilder RegisterRoute(IEndpointRouteBuilder routes)
    {
        var routeAttributes = GetType()
            .GetCustomAttributes(typeof(RouteAttribute), false)
            .Select(x => (RouteAttribute) x)
            .ToArray();
        var cacheAttributes = GetType()
            .GetCustomAttributes(typeof(CACHEAttribute), false)
            .Select(x => (CACHEAttribute) x)
            .ToArray();

        if (routeAttributes.Length == 0)
        {
            throw new MissingRouteAttributeException(GetType());
        }
        if (routeAttributes.Length > 1)
        {
            throw new DuplicateRouteAttributeException(GetType());
        }
        if (cacheAttributes.Length > 1)
        {
            throw new DuplicateCacheAttributeException(GetType());
        }

        var routeAttribute = routeAttributes.Single()!;

        var convention = routeAttribute.Register(routes, async ([AsParameters] TContract request, IMediator mediator, CancellationToken cancellationToken)
            => await (this as IHttpEndpoint<TContract>).HandleAsync(request, mediator, cancellationToken));

        if (cacheAttributes.Length == 1)
        {
            var cacheExpiration = cacheAttributes[0].Expiration;
            convention = convention.CacheOutput(policy => policy.Cache().Expire(cacheExpiration));
        }

        return convention;
    }

    async Task<IResult> IHttpEndpoint<TContract>.HandleAsync(TContract requestContract, IMediator mediator, CancellationToken cancellationToken)
    {
        var request = Mapper.Map<TContract, TRequest>(requestContract);
        var mediatrResult = await mediator.Send(request, cancellationToken);

        return mediatrResult.Status switch
        {
            ResultStatus.Value => MapToResponse(((MediatrValueResult<TResult>)mediatrResult).Value),
            ResultStatus.Unauthorized => Results.Problem(
                statusCode: StatusCodes.Status401Unauthorized,
                title: "Unauthorized",
                type: "https://tools.ietf.org/html/rfc7235#section-3.1"
            ),
            ResultStatus.Forbidden => Results.Problem(
                statusCode: StatusCodes.Status403Forbidden,
                title: "Forbidden",
                type: "https://tools.ietf.org/html/rfc7231#section-6.5.3"
            ),
            ResultStatus.ValidationFailed => Results.ValidationProblem(
                statusCode: StatusCodes.Status400BadRequest,
                type: "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                errors: ((MediatrValidationFailedResult)mediatrResult).Errors
            ),
            _ => throw new InvalidOperationException(),
        };
    }
}
