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

        TEntity Update(TEntity obj);

        IList<TEntity> BulkUpdate(IList<TEntity> objs);
        Task DeleteById<T>(T id, CancellationToken cancellationToken = default);
        void ClearTracked();

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        Task<IDbContextTransaction> BeginTransaction(CancellationToken cancellationToken = default);
    }
}
