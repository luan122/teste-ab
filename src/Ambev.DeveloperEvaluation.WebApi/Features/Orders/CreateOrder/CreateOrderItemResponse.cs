namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateOrder
{
    /// <summary>
    /// API response model for order item information in order operations
    /// </summary>
    public class CreateOrderItemResponse
    {
        /// <summary>
        /// The name of the product
        /// </summary>
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// The quantity of the product ordered
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// The original unit price of the product
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// The discounted unit price of the product
        /// </summary>
        public decimal DiscountedPrice { get; set; }

        /// <summary>
        /// The total price for this item before discount
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// The total price for this item after applying discount
        /// </summary>
        public decimal TotalPriceWithDiscount { get; set; }

        /// <summary>
        /// Optional description or notes for the order item
        /// </summary>
        public string? Description { get; set; }
    }
}
