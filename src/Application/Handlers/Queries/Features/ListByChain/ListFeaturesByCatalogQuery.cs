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
        public required ulong ChainId { get; init; }
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
        ChainDeploymentNotFound,
        CatalogNotFound
    }

    public record Result(Status Status, FeatureDeploymentDTO[]? Features);

    public class Handler : QueryHandler<Request, Result>
    {
        private readonly IAppDbContext DbContext;
        public readonly IMapper Mapper;

        public Handler(IAppDbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            Mapper = mapper;
        }

        public override async Task<Result> HandleAsync(Request request, CancellationToken cancellationToken)
        {
            var features = await DbContext.FeatureDeployments
                .Where(x => x.ChainId == request.ChainId && x.CatalogAddress.ToUpper() == request.CatalogAddress.ToUpper())
                .ProjectTo<FeatureDeploymentDTO>(Mapper.ConfigurationProvider)
                .ToArrayAsync(cancellationToken: cancellationToken);

            if (features.Length > 0)
            {
                return new Result(Status.Success, features);
            }

            if (!await DbContext.CatalogDeployments
                .AnyAsync(x => x.ChainId == request.ChainId && x.Address == request.CatalogAddress, cancellationToken: cancellationToken))
            {
                return new Result(Status.CatalogNotFound, null);
            }

            if (!await DbContext.CatalogDeployments
                .AnyAsync(x => x.ChainId == request.ChainId && x.Address == request.CatalogAddress, cancellationToken: cancellationToken))
            {
                return new Result(Status.CatalogNotFound, null);
            }

            //
            return new Result(Status.Success, Array.Empty<FeatureDeploymentDTO>());
        }
    }
}

