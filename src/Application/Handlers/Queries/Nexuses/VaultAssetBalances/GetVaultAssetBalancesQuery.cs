using FluentValidation;
using Nethereum.ABI;
using Nexus.Application.Common;
using Nexus.Application.DTOs;
using Nexus.Application.Services;
using Nexus.Application.Services.Contracts;

namespace Nexus.Application.Handlers.Queries;
public class GetVaultAssetBalancesQuery
{
    public class Request : Query<Result>
    {
        public required ushort ContractChainId { get; init; }
        public required string NexusAddress { get; init; }

        public required ushort SubchainContractChainId { get; init; }
        public required V1TokenTypes TokenType { get; init; }
        public required string TokenIdentifier { get; init; }

        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(x => x.NexusAddress).NotNull().NotEmpty();
                RuleFor(x => x.TokenType).IsInEnum();
                RuleFor(x => x.TokenIdentifier).NotNull();
            }
        }
    }

    public enum Status : byte
    {
        Success,
        UnsupportedChain,
        NexusNotFound
    }

    public record Result(Status Status, VaultAssetBalanceDTO[] VaultBalances = null!);

    public class Handler : QueryHandler<Request, Result>
    {
        private readonly IVaultV1ControllerProvider VaultControllerProvider;
        private readonly IWeb3ProviderService Web3ProviderService;
        private readonly INexusFactoryProvider NexusFactoryProvider;
        private readonly IVaultV1Provider VaultV1Provider;
        private readonly ABIEncode ABIEncode;

        public Handler(IVaultV1ControllerProvider vaultControllerProvider, IWeb3ProviderService web3ProviderService,
            INexusFactoryProvider nexusFactoryProvider, IVaultV1Provider vaultV1Provider, ABIEncode aBIEncode)
        {
            VaultControllerProvider = vaultControllerProvider;
            Web3ProviderService = web3ProviderService;
            NexusFactoryProvider = nexusFactoryProvider;
            VaultV1Provider = vaultV1Provider;
            ABIEncode = aBIEncode;
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

            byte[] nexusId = ABIEncode.GetSha3ABIEncodedPacked(
                new ABIValue("uint16", request.ContractChainId), new ABIValue("address", request.NexusAddress));

            var vaultController = VaultControllerProvider.GetInstance(request.SubchainContractChainId);
            var vaultInfos = await vaultController.GetVaultsAsync(nexusId);
            var tokenInfo = new V1TokenInfoDTO()
            {
                TokenType = request.TokenType,
                TokenIdentifier = request.TokenIdentifier,
            };

            var balances = await Task.WhenAll(vaultInfos.Select(async vaultInfo =>
            {
                var vault = VaultV1Provider.GetInstance(request.SubchainContractChainId, vaultInfo.Vault);
                var balance = await vault.GetBalanceAsync(request.TokenType, request.TokenIdentifier);

                return new VaultAssetBalanceDTO(vaultInfo, tokenInfo, balance);
            }));

            return new Result(Status.Success, balances);
        }
    }
}

