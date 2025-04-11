using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DefaultContext _context;

        public CategoryRepository(DefaultContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Creates a new category in the database
        /// </summary>
        /// <param name="category">The category to create</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created <see cref="Category" /></returns>
        public async Task<Category> CreateAsync(Category category, CancellationToken cancellationToken = default)
        {
            await _context.Categories.AddAsync(category, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return category;
        }

        /// <summary>
        /// Retrieves a category by their unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the category</param>
        /// <param name="tracking">If the entity should be tracked or not</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The <see cref="Category" /> with the specified id if found, <see langword="null" /> otherwise.</returns>
        public async Task<Category?> GetByIdAsync(Guid id, bool tracking = false, CancellationToken cancellationToken = default)
        {
            var query = tracking ? _context.Categories : _context.Categories.AsNoTracking();
            return await query.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        /// <summary>
        /// Retrieves a category by its title.
        /// </summary>
        /// <param name="title">The title of the category to retrieve.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="Category" /> with the specified title if found, <see langword="null" /> otherwise.</returns>
        public async Task<Category?> GetByTitleAsync(string title, CancellationToken cancellationToken = default)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Title == title, cancellationToken);
        }

        /// <summary>
        /// Soft deletes a category from the database
        /// </summary>
        /// <param name="id">The unique identifier of the category to delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns><see langword="true"/> if the category was deleted, <see langword="false"/> if not found</returns>
        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var category = await GetByIdAsync(id, true, cancellationToken);
            if (category == null)
                return false;
            category.IsDeleted = true;
            category.UpdatedAt = DateTime.Now;
            var result = await _context.SaveChangesAsync(cancellationToken);
            return result > 0;
        }
    }
}
