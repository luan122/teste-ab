namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateOrder
{
    /// <summary>
    /// Represents a request to create a new order in the system.
    /// </summary>
    public class CreateOrderRequest
    {
        /// <summary>
        /// Gets or sets the unique identifier of the customer placing the order.
        /// </summary>
        public Guid CustomerId { get; set; } = Guid.Empty;

        /// <summary>
        /// Gets or sets the unique identifier of the branch where the order is placed.
        /// </summary>
        public Guid BranchId { get; set; } = Guid.Empty;

        /// <summary>
        /// Gets or sets the collection of items to be included in the order.
        /// </summary>
        public virtual List<CreateOrderItemRequest> Items { get; set; } = []!;
    }
}
