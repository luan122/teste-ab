namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateOrder
{
    /// <summary>
    /// API response model for CreateOrder operation
    /// </summary>
    public class CreateOrderResponse
    {
        /// <summary>
        /// The unique identifier of the created order
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The sequential order number
        /// </summary>
        public int OrderNumber { get; set; }

        /// <summary>
        /// The customer information for this order
        /// </summary>
        public CreateOrderUserResponse Customer { get; set; }

        /// <summary>
        /// The branch information where the order was placed
        /// </summary>
        public CreateOrderBranchResponse Branch { get; set; }

        /// <summary>
        /// The total price of the order before discounts
        /// </summary>
        public decimal TotalOrderPrice { get; set; }

        /// <summary>
        /// The total price of the order after applying discounts
        /// </summary>
        public decimal TotalOrderPriceWithDiscount { get; set; }

        /// <summary>
        /// The list of items in the order
        /// </summary>
        public virtual List<CreateOrderItemResponse> Items { get; set; } = []!;

        /// <summary>
        /// The date when the order was created
        /// </summary>
        public DateOnly CreatedAt { get; set; }
    }
}
