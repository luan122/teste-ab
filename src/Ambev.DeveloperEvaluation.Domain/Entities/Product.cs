using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities.Aggregates;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Product : BaseWithAuditEntity
    {
        public Guid CategoryId { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; }
        public bool IsActive { get; set; } = true!;
        public virtual Category Category { get; set; }
        public virtual ProductRate Rating { get; set; } = null!;
        public Product()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}
