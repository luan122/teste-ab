using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface IBaseRepository<TEntity> : IDisposable, IAsyncDisposable
        where TEntity : class
    {
        DbSet<TEntity> Database { get; }
        Task<TEntity> AddAsync(TEntity obj, CancellationToken cancellationToken = default);

        Task<IList<TEntity>> BulkAddAsync(IList<TEntity> objs, CancellationToken cancellationToken = default);

        Task<IList<TEntity>> FindAsync<T>(Expression<Func<TEntity, bool>> predicate, bool track = false, CancellationToken cancellationToken = default);
        Task<TEntity?> FindOneAsync<T>(Expression<Func<TEntity, bool>> predicate, bool track = false, CancellationToken cancellationToken = default);

        TEntity Update(TEntity obj);

        IList<TEntity> BulkUpdate(IList<TEntity> objs);

        void ClearTracked();

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        Task<IDbContextTransaction> BeginTransaction(CancellationToken cancellationToken = default);
    }
}
