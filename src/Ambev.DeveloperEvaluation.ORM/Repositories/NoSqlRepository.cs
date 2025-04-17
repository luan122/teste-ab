using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver.GridFS;
using System;
using System.Linq;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class NoSqlRepository<TEntity, TContext> : INoSqlRepository<TEntity>
        where TEntity : class
        where TContext : NoSQLContext
    {
        private TContext Context;
        public DbSet<TEntity> Database { get; private set; }
        public IGridFSBucket GridFsBucket => Context.GridFsBucket;

        public NoSqlRepository(TContext context)
        {
            Context = context;
            Database = context.Set<TEntity>();
        }

        public IMongoClient Client => Context.Client;
        public async Task<bool> UpdateGridFsFileByFileName(string currentFileName, string newFileName, CancellationToken cancellationToken)
        {
            var filter = Builders<GridFSFileInfo>.Filter.Eq("filename", currentFileName);
            using var results = await GridFsBucket.FindAsync(filter, cancellationToken: cancellationToken).ConfigureAwait(false);
            var count = 0;
            while (await results.MoveNextAsync(cancellationToken).ConfigureAwait(false))
            {
                count++;
                foreach (var item in results.Current)
                {
                    await GridFsBucket.RenameAsync(item.Id, newFileName, cancellationToken).ConfigureAwait(false);
                }
            }
            return count != 0;
        }
        public async Task<bool> DeleteGridFsByFileName(string fileName, CancellationToken cancellationToken)
        {
            var filter = Builders<GridFSFileInfo>.Filter.Eq("filename", fileName);
            using var results = await GridFsBucket.FindAsync(filter, cancellationToken: cancellationToken).ConfigureAwait(false);

            var count = 0;
            while (await results.MoveNextAsync(cancellationToken).ConfigureAwait(false))
            {
                count++;
                foreach (var item in results.Current)
                {
                    await GridFsBucket.DeleteAsync(item.Id, cancellationToken).ConfigureAwait(false);
                }
            }
            return count != 0;
        }

        public void Dispose()
        {
            Context?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            await DisposeAsyncCore().ConfigureAwait(false);
            GC.SuppressFinalize(this);
        }

        protected virtual async ValueTask DisposeAsyncCore()
        {
            if (Context != null)
            {
                await Context.DisposeAsync().ConfigureAwait(false);
                Context = null!;
            }
        }
    }
}
