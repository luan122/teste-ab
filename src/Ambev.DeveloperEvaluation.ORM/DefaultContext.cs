using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Entities.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SharpCompress.Common;
using System.Linq.Expressions;
using System.Reflection;

namespace Ambev.DeveloperEvaluation.ORM;

public class DefaultContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Produts { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
        var deletedPropertyName = "IsDeleted";
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            var parameter = Expression.Parameter(entity.ClrType, "e");
            if (entity.ClrType.IsSubclassOf(typeof(BaseWithAuditEntity)) || entity.ClrType.IsSubclassOf(typeof(IBaseWithCreatedAtEntity)))
            {
                var prop = entity.ClrType.GetProperty(deletedPropertyName);
                if (prop != null)
                {
                    var expBody = Expression.Equal(
                        Expression.Call(typeof(EF), nameof(EF.Property), [prop.PropertyType], parameter, Expression.Constant(deletedPropertyName)),
                        Expression.Constant(false)
                        );
                    var exp = Expression.Lambda(expBody, parameter);
                    modelBuilder.Entity(entity.ClrType).HasQueryFilter(exp);
                }
                modelBuilder.Entity(entity.ClrType).Property("CreatedAt")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                modelBuilder.Entity(entity.ClrType).Property("IsDeleted")
                    .HasDefaultValue(false);
            }
            if (entity.ClrType.IsSubclassOf(typeof(BaseEntity)))
            {
                modelBuilder.Entity(entity.ClrType).HasKey("Id");

                modelBuilder.Entity(entity.ClrType).Property("Id")
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()");
            }
        }
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