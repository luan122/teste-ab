using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Category entity operations
/// </summary>
public interface ICategoryRepository
{
    /// <summary>
    /// Creates a new category in the repository
    /// </summary>
    /// <param name="category">The category to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created category</returns>
    Task<Category> CreateAsync(Category category, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a category by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the category</param>
    /// <param name="tracking">If the entity should be tracked or not</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The category if found, null otherwise</returns>
    Task<Category?> GetByIdAsync(Guid id, bool tracking = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a category by its title.
    /// </summary>
    /// <param name="title">The title of the category to retrieve.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>The <see cref="Category" /> with the specified title if found, <see langword="null" /> otherwise.</returns>
    Task<Category?> GetByTitleAsync(string title, CancellationToken cancellationToken = default);

    /// <summary>
    /// Soft deletes a category from the repository
    /// </summary>
    /// <param name="id">The unique identifier of the category to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the category was deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
