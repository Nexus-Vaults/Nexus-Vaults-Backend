using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Nexus.Application.Common;
using Nexus.Application.Services;
using Nexus.Domain.Common;
using Nexus.Infrastructure.Persistence.Concurrency;
using System.Reflection;
using System.Transactions;

namespace Nexus.Infrastructure.Persistence;
public class AppDbContext : MergingDbContext, IAppDbContext
{

    public AppDbContext(
        DbContextOptions<AppDbContext> options,
        Random random,
        PropertyMerger propertyMerger)
        : base(options, random, propertyMerger)
    {
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.IgnoreAny<IList<DomainEvent>>();

        base.ConfigureConventions(configurationBuilder);
    }

    public async Task<DbSaveResult> SaveChangesAsync(DbStatus allowedStatuses = DbStatus.Success, bool discardConcurrentDeletedEntries = false,
            IDbContextTransaction? transaction = null, TransactionScope? transactionScope = null,
            CancellationToken cancellationToken = new CancellationToken())
    {
        EnsureTransactionIsUsed(transaction);
        EnsureNoNestedTransactions(transaction, transactionScope);

        try
        {
            var result = await base.SaveChangesAsync(allowedStatuses, discardConcurrentDeletedEntries, cancellationToken: cancellationToken);

            if (result.Status == DbStatus.Success &&
                (transaction is not null || transactionScope is not null))
            {
                if (transaction is not null)
                {
                    await transaction.CommitAsync(cancellationToken);
                }

                transactionScope?.Complete();
            }

            return result;
        }
        finally
        {
            if (transaction is not null)
            {
                await transaction.DisposeAsync();
            }

            transactionScope?.Dispose();
        }
    }

    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        return Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CloseTransactionAsync(IDbContextTransaction? transaction = null, TransactionScope? transactionScope = null,
        CancellationToken cancellationToken = default)
    {
        EnsureTransactionIsUsed(transaction);
        EnsureNoNestedTransactions(transaction, transactionScope);

        try
        {
            if (transaction is not null)
            {
                await transaction.CommitAsync(cancellationToken);
            }

            transactionScope?.Complete();
        }
        finally
        {
            if (transaction is not null)
            {
                await transaction.DisposeAsync();
            }

            transactionScope?.Dispose();
        }
    }

    private void EnsureTransactionIsUsed(IDbContextTransaction? transaction)
    {
        if (transaction is null)
        {
            return;
        }
        if (transaction != Database.CurrentTransaction)
        {
            throw new InvalidOperationException("This context does not use the given transaction!");
        }
    }
    private static void EnsureNoNestedTransactions(IDbContextTransaction? transaction, TransactionScope? transactionScope)
    {
        if (transaction is null || transactionScope is null)
        {
            return;
        }

        throw new InvalidOperationException("Dont combine DbContexTransactions and TransactionScopes");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
