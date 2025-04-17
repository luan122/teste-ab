using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Orders.Commands.GetOrder
{
    public class GetOrderItemResult
    {
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountedPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalPriceWithDiscount { get; set; }
        public string? Description { get; set; }
    }
}
