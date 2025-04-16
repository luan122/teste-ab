using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Entities.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Ambev.DeveloperEvaluation.ORM;

public class DefaultContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Produts { get; set; }

    public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
        UpdateEntitiesBeforeSave();
        return base.SaveChanges();
    }
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        UpdateEntitiesBeforeSave();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        UpdateEntitiesBeforeSave();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateEntitiesBeforeSave();
        return base.SaveChangesAsync(cancellationToken);
    }
    private void UpdateEntitiesBeforeSave()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Modified && entry.Entity.GetType().GetProperties().Any(a => a.Name.Contains("UpdatedAt", StringComparison.InvariantCultureIgnoreCase)))
            {
                if (entry.Entity is IBaseWithUpdatedAtEntity entity)
                {
                    entity.UpdatedAt = DateTime.UtcNow;
                }
            }
            if (entry.State == EntityState.Deleted)
            {
                if (entry.Entity is BaseWithAuditEntity entity)
                {
                    entity.UpdatedAt = DateTime.UtcNow;
                    entity.IsDeleted = true;
                    entry.State = EntityState.Modified;
                }
            }
        }
    }
}
public class YourDbContextFactory : IDesignTimeDbContextFactory<DefaultContext>
{
    public DefaultContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<DefaultContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        builder.UseNpgsql(
               connectionString,
               b => b.MigrationsAssembly("Ambev.DeveloperEvaluation.ORM"));

        return new DefaultContext(builder.Options);
    }
}