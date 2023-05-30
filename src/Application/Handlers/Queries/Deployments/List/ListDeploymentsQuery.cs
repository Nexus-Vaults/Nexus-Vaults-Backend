using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Nexus.Application.Common;
using Nexus.Application.DTOs;
using Nexus.Application.Services;

namespace Nexus.Application.Handlers.Queries.Deployments.List;
public class ListDeploymentsQuery
{
    public class Request : Query<Result>
    {
    }

    public enum Status : byte
    {
        Success
    }

    public record Result(Status Status, ChainDeploymentDTO[] Deployments);

    public class Handler : QueryHandler<Request, Result>
    {
        private readonly IAppDbContext DbContext;
        private readonly IMapper Mapper;

        public Handler(IAppDbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            Mapper = mapper;
        }

        public override async Task<Result> HandleAsync(Request request, CancellationToken cancellationToken)
        {
            var deployments = await DbContext.ChainDeployments
                .ProjectTo<ChainDeploymentDTO>(Mapper.ConfigurationProvider)
                .ToArrayAsync(cancellationToken: cancellationToken);

            return new Result(Status.Success, deployments);
        }
    }
}
