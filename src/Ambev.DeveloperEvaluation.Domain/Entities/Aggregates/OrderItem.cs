using Ambev.DeveloperEvaluation.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Aggregates
{
    public class OrderItem : BaseWithAuditEntity
    {
        public Guid OrderId { get; set; } = Guid.Empty!;
        public Guid ProductId { get; set; } = Guid.Empty!;
        public int Quantity { get; set; }
        public string? Description { get; set; }
        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
        public decimal GetTotalPrice()
        {
            return Product == default ?
                0
                : Product.Price * Quantity;
        }
        public decimal GetITotalPriceWithDiscount()
        {
            return Product == default ?
                0
                : (Product.Price - GetDiscountedPrice()) * Quantity;
        }
        public decimal GetDiscountedPrice()
        {
            return this switch
            {
                { Quantity: >= 4 and <= 9 } => Product.Price * 0.1m,
                { Quantity: >= 10 and <= 20 } => Product.Price * 0.2m,
                _ => 0
            };
        }
    }
}
