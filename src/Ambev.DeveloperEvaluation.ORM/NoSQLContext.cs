using Ambev.DeveloperEvaluation.Domain.Entities.Aggregates;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Reflection;

using Microsoft.Extensions.Configuration;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Bson;

namespace Ambev.DeveloperEvaluation.ORM
{
    public class NoSQLContext(DbContextOptions options, IConfiguration configuration) : DbContext(options)
    {
        private IGridFSBucket? _gridFsBucket;
        private IMongoClient? _client;
        private string _databaseName => GetDatabaseName();
        private MongoUrl _connection => GetMongoConnection();
        public IGridFSBucket GridFsBucket => GetGridFS();
        public IMongoClient Client => GetMongoClient();
        public string DatabaseName => _databaseName;
        private IGridFSBucket GetGridFS()
        {
            if (_gridFsBucket == null)
            {
                var database = Client.GetDatabase(_databaseName);
                _gridFsBucket = new GridFSBucket(database);
            }
            return _gridFsBucket;
        }
        private IMongoClient GetMongoClient()
        {
            if (_client == null)
            {
                var settings = MongoClientSettings.FromUrl(_connection);
                _client = new MongoClient(settings);
            }
            return _client;
        }
        private MongoUrl GetMongoConnection()
        {
            return new MongoUrl(configuration.GetConnectionString("MongoDb"));
        }
        private string GetDatabaseName()
        {
            return _connection.DatabaseName;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        public IGridFSBucket GetNamedGridFs(string gridName)
        {
            var database = Client.GetDatabase(_databaseName);
            return new GridFSBucket(database, new GridFSBucketOptions
            {
                BucketName = gridName
            });
        }
    }
}
