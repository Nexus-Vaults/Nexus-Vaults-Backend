using FluentValidation;
using Nethereum.Util;
using Nexus.Application.Common;
using Nexus.Application.Services;
using Nexus.Application.Services.Contracts;
using System;
using System.Threading.Tasks;

namespace Nexus.Application.Handlers.Queries;
public class GetNexusExistenceQuery
{
    public class Request : Query<Result>
    {
        public required ushort ContractChainId { get; init; }
        public required string NexusAddress { get; init; }

        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(x => x.NexusAddress)
                    .Must(AddressUtil.Current.IsValidEthereumAddressHexFormat)
                    .WithMessage("Invalid address format");
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
        private readonly INexusFactoryProvider NexusFactoryProvider;
        private readonly IWeb3ProviderService Web3ProviderService;

        public Handler(INexusFactoryProvider nexusFactoryProvider, IWeb3ProviderService web3ProviderService)
        {
            NexusFactoryProvider = nexusFactoryProvider;
            Web3ProviderService = web3ProviderService;
        }

        public override async Task<Result> HandleAsync(Request request, CancellationToken cancellationToken)
        {
            if (!Web3ProviderService.IsSupported(request.ContractChainId))
            {
                return new Result(Status.UnsupportedChain);
            }

            var nexusFactory = NexusFactoryProvider.GetInstance(request.ContractChainId);

            return !await nexusFactory.HasDeployedAsync(request.NexusAddress) 
                ? new Result(Status.NexusNotFound) 
                : new Result(Status.Success);
        }
    }
}

