namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateOrder
{
    /// <summary>
    /// Represents an item to be included in a new order request.
    /// </summary>
    public class CreateOrderItemRequest
    {
        /// <summary>
        /// Gets or sets the unique identifier of the product to be ordered.
        /// </summary>
        public Guid ProductId { get; set; } = Guid.Empty;

        /// <summary>
        /// Gets or sets the quantity of the product to be ordered.
        /// </summary>
        public int Quantity { get; set; }
    }
}
