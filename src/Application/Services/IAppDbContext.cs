using Microsoft.EntityFrameworkCore.Storage;
using Nexus.Application.Common;
using System.Transactions;

namespace Nexus.Application.Services;
public interface IAppDbContext
{

    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task<DbSaveResult> SaveChangesAsync(DbStatus allowedStatuses = DbStatus.Success, bool discardConcurrentDeletedEntries = false,
                IDbContextTransaction? transaction = null, TransactionScope? transactionScope = null,
                CancellationToken cancellationToken = new CancellationToken());
}
