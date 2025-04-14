using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using System.Linq.Expressions;
using Ambev.DeveloperEvaluation.Domain.Common;
using System.IO;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class BaseRepository<TEntity, TContext> : IBaseRepository<TEntity>, IDisposable, IAsyncDisposable
        where TEntity : class
        where TContext : DbContext
    {
        public TContext Context { get; private set; }
        public DbSet<TEntity> Database { get; private set; }

        public BaseRepository(TContext context)
        {
            Context = context;
            Database = context.Set<TEntity>();
        }

        public virtual async Task<TEntity> AddAsync(TEntity obj, CancellationToken cancellationToken = default)
        {
            await Database.AddAsync(obj, cancellationToken);
            return obj;
        }

        public virtual async Task<IList<TEntity>> BulkAddAsync(IList<TEntity> objs, CancellationToken cancellationToken = default)
        {
            await Database.AddRangeAsync(objs, cancellationToken);
            return objs;
        }

        public virtual async Task<IList<TEntity>> FindAsync<T>(Expression<Func<TEntity, bool>> predicate, bool track = false, CancellationToken cancellationToken = default)
        {
            return track ?
                await Database.Where(predicate).ToListAsync(cancellationToken) :
                await Database.AsNoTracking().Where(predicate).ToListAsync(cancellationToken);
        }

        public virtual async Task<TEntity?> FindOneAsync<T>(Expression<Func<TEntity, bool>> predicate, bool track = false, CancellationToken cancellationToken = default)
        {
            return track ?
                await Database.FirstOrDefaultAsync(predicate, cancellationToken) :
                await Database.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public virtual TEntity Update(TEntity obj)
        {
            Database.Update(obj);
            return obj;
        }

        public virtual IList<TEntity> BulkUpdate(IList<TEntity> objs)
        {
            Database.UpdateRange(objs);
            return objs;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await Context.SaveChangesAsync(cancellationToken);
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
            Context.Database.CurrentTransaction?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            await DisposeAsyncCore().ConfigureAwait(false);
        }
        protected virtual async ValueTask DisposeAsyncCore()
        {
            if (Context != null)
            {
                await Context.DisposeAsync().ConfigureAwait(false);
                if (Context.Database.CurrentTransaction != null)
                {
                    await Context.Database.CurrentTransaction.DisposeAsync().ConfigureAwait(false);
                }
            }
            Context = null!;
            Database = null!;
        }

        public virtual void ClearTracked()
        {
            Context.ChangeTracker.Clear();
        }

        public async Task<IDbContextTransaction> BeginTransaction(CancellationToken cancellationToken = default)
        {
            return await Context.Database.BeginTransactionAsync(cancellationToken);
        }
    }
}
