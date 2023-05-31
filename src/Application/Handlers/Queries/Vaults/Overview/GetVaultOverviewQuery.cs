using FluentValidation;
using Nexus.Application.Common;


namespace Nexus.Application.Handlers.Queries.Vaults.Overview;
public class GetVaultOverviewQuery
{
    public class Request : Query<Result>
    {
        public required ushort NexusContractChainId { get; init; }
        public required string NexusAddress { get; init; }
        public required ushort VaultContractChainId { get; init; }
        public required uint VaultId { get; init; }

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
        VaultNotFound
    }

    public record Result(Status Status, string VaultAddress);

    public class Handler : QueryHandler<Request, Result>
    {
        public override async Task<Result> HandleAsync(Request request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

