using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface INoSqlRepository<TEntity> : IDisposable, IAsyncDisposable
        where TEntity: class
    {
        IGridFSBucket GridFsBucket { get; }
        DbSet<TEntity> Database { get; }
        IMongoClient Client { get; }
        Task<bool> DeleteGridFsByFileName(string imageName, CancellationToken cancellationToken);
        Task<bool> UpdateGridFsFileByFileName(string currentFileName, string newFileName, CancellationToken cancellationToken);
    }
}
