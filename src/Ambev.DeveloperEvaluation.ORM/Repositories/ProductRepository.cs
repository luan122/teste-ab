using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class ProductRepository(DefaultContext context) : BaseRepository<Product, DefaultContext>(context), IProductRepository
    {
        public async Task AddProduct(Product entity, CancellationToken cancellationToken = default)
        {
            var category = await Context.Categories.FirstOrDefaultAsync(fd => fd.Title == entity.Category.Title, cancellationToken: cancellationToken);
            if (category != null)
                entity.Category = category;
            await Database.AddAsync(entity, cancellationToken);

        }

        public async Task<ProductUpdateCategoryOperation> UpdateProductCategory(Product currentEntity, Product updatedEntity, CancellationToken cancellationToken = default)
        {
            var category = await Context.Categories.AsNoTracking().FirstOrDefaultAsync(fd => fd.Title == updatedEntity.Category.Title, cancellationToken: cancellationToken);
            if (category == null)
            {
                await Context.Categories.AddAsync(updatedEntity.Category, cancellationToken);
                currentEntity.Category = updatedEntity.Category;
                currentEntity.CategoryId = updatedEntity.CategoryId;
                return ProductUpdateCategoryOperation.Add;

            }
            else if (category != null && currentEntity.CategoryId == category.Id)
                return ProductUpdateCategoryOperation.Keep;
            else
            {
                currentEntity.Category = category;
                currentEntity.CategoryId = category.Id;
                return ProductUpdateCategoryOperation.Update;
            }
        }

        public async Task DeleteImage(Guid id, CancellationToken cancellationToken = default)
        {
            var product = await Database.Include(fd => fd.Image).FirstOrDefaultAsync(fd => fd.Id == id, cancellationToken: cancellationToken);
            if (product == null)
                return;
            product.Image = null;
        }
    }
}
