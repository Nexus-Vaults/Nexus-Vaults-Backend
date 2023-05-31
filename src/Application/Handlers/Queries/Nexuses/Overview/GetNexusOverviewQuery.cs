using FluentValidation;
using Nethereum.ABI;
using Nexus.Application.Common;
using Nexus.Application.Services;
using Nexus.Application.Services.Contracts;

namespace Nexus.Application.Handlers.Queries.Nexus.Overview;
public class GetNexusOverviewQuery
{
    public class Request : Query<Result>
    {
        public required ushort ContractChainId { get; init; }
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

    public record Result(Status Status, string? NexusId = null, string? Name = null, string? Owner = null, VaultInfoDTO[]? Vaults = null);

    public class Handler : QueryHandler<Request, Result>
    {
        private readonly IWeb3ProviderService Web3ProviderService;
        private readonly INexusFactoryProvider NexusFactoryProvider;
        private readonly IVaultV1ControllerProvider VaultV1ControllerProvider;
        private readonly INexusProvider NexusProvider;
        private readonly ABIEncode ABIEncode;

        public Handler(IWeb3ProviderService web3ProviderService, INexusFactoryProvider nexusFactoryProvider,
            IVaultV1ControllerProvider vaultV1ControllerProvider, INexusProvider nexusProvider, ABIEncode abiEncode)
        {
            Web3ProviderService = web3ProviderService;
            NexusFactoryProvider = nexusFactoryProvider;
            VaultV1ControllerProvider = vaultV1ControllerProvider;
            NexusProvider = nexusProvider;
            ABIEncode = abiEncode;
        }

        public override async Task<Result> HandleAsync(Request request, CancellationToken cancellationToken)
        {
            if (!Web3ProviderService.IsSupported(request.ContractChainId))
            {
                return new Result(Status.UnsupportedChain);
            }

            var nexusFactory = NexusFactoryProvider.GetInstance(request.ContractChainId);

            if (!await nexusFactory.HasDeployedAsync(request.NexusAddress))
            {
                return new Result(Status.NexusNotFound);
            }

            var nexus = NexusProvider.GetInstance(request.ContractChainId, request.NexusAddress);

            string nexusName = await nexus.GetNameAsync();
            string nexusOwner = await nexus.GetOwnerAsync();

            byte[] nexusId = ABIEncode.GetSha3ABIEncodedPacked(request.ContractChainId, request.NexusAddress);
            var controllers = VaultV1ControllerProvider.GetAllInstances();

            var vaults = (
                await Task.WhenAll(controllers.Select(controller => controller.GetVaultsAsync(nexusId)))
            )
            .SelectMany(x => x)
            .ToArray();

            return new Result(Status.Success, Convert.ToHexString(nexusId), nexusName, nexusOwner, vaults);
        }
    }
}

