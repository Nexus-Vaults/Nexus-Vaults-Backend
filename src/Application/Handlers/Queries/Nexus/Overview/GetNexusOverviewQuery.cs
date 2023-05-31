using FluentValidation;
using Nexus.Application.Common;
using Nexus.Application.Services;
using Nexus.Application.Services.Contracts;

namespace Nexus.Application.Handlers.Queries.Nexus.Overview;
public class GetNexusOverviewQuery
{
    public class Request : Query<Result>
    {
        public required ulong ChainId { get; init; }
        public required string NexusAddress { get; init; }

        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }
    }

    public enum Status : byte
    {
        Success,
        UnsupportedChain,
        NexusNotFound
    }

    public record Result(Status Status);

    public class Handler : QueryHandler<Request, Result>
    {
        private readonly IWeb3ProviderService Web3ProviderService;
        private readonly INexusFactoryProvider NexusFactoryProvider;

        public Handler(IWeb3ProviderService web3ProviderService, INexusFactoryProvider nexusFactoryProvider)
        {
            Web3ProviderService = web3ProviderService;
            NexusFactoryProvider = nexusFactoryProvider;
        }

        public override async Task<Result> HandleAsync(Request request, CancellationToken cancellationToken)
        {
            if (!Web3ProviderService.IsSupported(request.ChainId))
            {
                return new Result(Status.UnsupportedChain);
            }

            var nexusFactory = NexusFactoryProvider.GetNexusFactory(request.ChainId);

            if (!await nexusFactory.HasDeployedAsync(request.NexusAddress))
            {
                return new Result(Status.NexusNotFound);
            }

            throw new NotImplementedException();
        }
    }
}

