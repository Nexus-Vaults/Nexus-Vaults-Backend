//using Farsight.Application.Common;
//using Farsight.Application.Common.MediatR;
//using Farsight.Application.Services;
//using MediatR;
//using System.Reflection;

//namespace Farsight.Application.Handlers;
//public class AuthorizationBehaviour<TRequest, TResult> : IPipelineBehavior<TRequest, MediatrResult>
//    where TRequest : IRequest<MediatrResult>
//{
//    private readonly ICurrentUserService CurrentUserService;
//    private readonly IRequestReflectorService RequestReflectorService;

//    public AuthorizationBehaviour(ICurrentUserService currentUserService, IRequestReflectorService requestReflectorService)
//    {
//        CurrentUserService = currentUserService;
//        RequestReflectorService = requestReflectorService;
//    }

//    public async Task<MediatrResult> Handle(TRequest request, RequestHandlerDelegate<MediatrResult> next, CancellationToken cancellationToken)
//    {
//        var declaringType = request.GetType().DeclaringType
//            ?? throw new Exception("Requests must be nested class declarations!");
//        var handlerMethod = RequestReflectorService.GetHandlerMethod(declaringType);

//        var authorizeAttributes = handlerMethod.GetCustomAttributes<AuthorizedAttribute>();

//        if (authorizeAttributes.Any())
//        {
//            // Must be authenticated user
//            if (CurrentUserService.GetUserId() is null)
//            {
//                return new MediatrResult(ResultStatus.Unauthorized);
//            }
//        }

//        var unauthorizedAttributes = handlerMethod.GetCustomAttributes<UnauthorizedAttribute>();

//        if (unauthorizedAttributes.Any())
//        {
//            // Must be unauthenticated user
//            if (CurrentUserService.GetUserId() is not null)
//            {
//                return new MediatrResult(ResultStatus.Forbidden);
//            }
//        }

//        // User is authorized / authorization not required
//        return await next();
//    }
//}