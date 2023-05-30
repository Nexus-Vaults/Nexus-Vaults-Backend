using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Nexus.Application.Common;
using Nexus.Domain.Entities.Deployment;
using System.Transactions;

namespace Nexus.Application.Services;
public interface IAppDbContext
{
    public DbSet<ChainDeployment> ChainDeployments { get; }
    public DbSet<CatalogDeployment> CatalogDeployments { get;  }
    public DbSet<FeatureDeployment> FeatureDeployments { get;  }

    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task<DbSaveResult> SaveChangesAsync(DbStatus allowedStatuses = DbStatus.Success, bool discardConcurrentDeletedEntries = false,
                IDbContextTransaction? transaction = null, TransactionScope? transactionScope = null,
                CancellationToken cancellationToken = new CancellationToken());
}
