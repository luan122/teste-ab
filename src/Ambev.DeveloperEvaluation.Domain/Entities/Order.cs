using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Order : BaseWithAuditEntity
    {
        public Guid CustomerId { get; set; } = Guid.Empty!;
        public Guid BranchId { get; set; } = Guid.Empty!;
        public int OrderNumber { get; set; }
        public bool IsCanceled { get; set; } = false!;
        public virtual User? Customer { get; set; }
        public virtual Branch? Branch { get; set; }
        public virtual List<OrderItem> Items { get; set; } = []!;
        public decimal GetOrderTotalPrice()
        {
            return Items.Sum(s => s.GetTotalPrice());
        }
        public decimal GetOrderTotalPriceWithDiscount()
        {
            return Items.Sum(s => s.GetITotalPriceWithDiscount());
        }
    }
}
