using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Nexus.Application.Common;

public static class MappingExtensions
{
    public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable,
        int pageNumber, int pageSize,
        CancellationToken cancellationToken = default)
    {
        return PaginatedList<TDestination>.CreateAsync(queryable, pageNumber, pageSize, cancellationToken);
    }
}