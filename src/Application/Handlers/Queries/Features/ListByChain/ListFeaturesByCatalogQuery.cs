using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Nexus.Application.Common;
using Nexus.Application.DTOs;
using Nexus.Application.Services;

namespace Nexus.Application.Handlers.Queries.Features.ListByChain;
public class ListFeaturesByCatalogQuery
{
    public class Request : Query<Result>
    {
        public required ushort ContractChainId { get; init; }
        public required string CatalogAddress { get; init; }

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
        CatalogNotFound
    }

    public record Result(Status Status, FeatureDeploymentDTO[]? Features);

    public class Handler : QueryHandler<Request, Result>
    {
        private readonly IWeb3ProviderService Web3ProviderService;
        private readonly IAppDbContext DbContext;
        public readonly IMapper Mapper;

        public Handler(IWeb3ProviderService web3ProviderService, IAppDbContext dbContext, IMapper mapper)
        {
            Web3ProviderService = web3ProviderService;
            DbContext = dbContext;
            Mapper = mapper;
        }

        public override async Task<Result> HandleAsync(Request request, CancellationToken cancellationToken)
        {
            if (!Web3ProviderService.IsSupported(request.ContractChainId))
            {
                return new Result(Status.UnsupportedChain, null);
            }

            var features = await DbContext.FeatureDeployments
                .Where(x => x.ContractChainId == request.ContractChainId && x.CatalogAddress.ToUpper() == request.CatalogAddress.ToUpper())
                .ProjectTo<FeatureDeploymentDTO>(Mapper.ConfigurationProvider)
                .ToArrayAsync(cancellationToken: cancellationToken);

            if (features.Length > 0)
            {
                return new Result(Status.Success, features);
            }

            if (!await DbContext.CatalogDeployments
                .AnyAsync(x => x.ContractChainId == request.ContractChainId && x.Address == request.CatalogAddress, cancellationToken: cancellationToken))
            {
                return new Result(Status.CatalogNotFound, null);
            }

            //
            return new Result(Status.Success, Array.Empty<FeatureDeploymentDTO>());
        }
    }
}

