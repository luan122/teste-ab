namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.CancelOrderItem
{
    /// <summary>
    /// Request model for cancel a order item by ID
    /// </summary>
    public class CancelOrderItemRequest
    {
        /// <summary>
        /// The unique identifier of the order
        /// </summary>
        public Guid OrderId { get; set; }
        /// <summary>
        /// The order number of the order
        /// </summary>
        public int OrderNumber { get; set; }

        /// <summary>
        /// The unique identifier of the order item to cancel
        /// </summary>
        public Guid OrderItemId { get; set; }
        
        /// <summary>
        /// The unique identifier of the user making the request
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Flag to indicate if the user can bypass rules
        /// </summary>
        public bool UserBypass { get; set; }
    }
}
