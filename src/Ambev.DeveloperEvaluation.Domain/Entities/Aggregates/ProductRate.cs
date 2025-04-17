using Ambev.DeveloperEvaluation.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Aggregates
{
    public class ProductRate : BaseEntity, IBaseWithCreatedAtEntity, IBaseWithUpdatedAtEntity
    {
        public Guid ProductId { get; set; }
        public double Rate { get; set; }
        public int Count { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual Product Product { get; set; } = null!;
        public ProductRate()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}
