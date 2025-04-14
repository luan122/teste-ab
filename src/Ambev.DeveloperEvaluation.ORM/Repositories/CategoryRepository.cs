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
    public class CategoryRepository(DefaultContext context) : BaseRepository<Category, DefaultContext>(context), ICategoryRepository
    {
        /// <summary>
        /// Soft deletes a category from the database
        /// </summary>
        /// <param name="id">The unique identifier of the category to delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns><see langword="true"/> if the category was deleted, <see langword="false"/> if not found</returns>
        public async Task<bool> SoftDeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var category = await Database.FirstOrDefaultAsync(fd => fd.Id == id, cancellationToken: cancellationToken);
            if (category == null)
                return false;
            category.IsDeleted = true;
            category.UpdatedAt = DateTime.Now;
            var result = await Context.SaveChangesAsync(cancellationToken);
            return result > 0;
        }
    }
}
