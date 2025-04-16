using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task AddProduct(Product entity, CancellationToken cancellationToken = default);
        Task<bool> UpdateProductCategory(Product currentEntity, Product updatedEntity, CancellationToken cancellationToken = default);
    }
}
