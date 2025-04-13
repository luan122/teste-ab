using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Category entity operations
/// </summary>
public interface ICategoryRepository : IBaseRepository<Category>
{

    Task<bool> SoftDeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
