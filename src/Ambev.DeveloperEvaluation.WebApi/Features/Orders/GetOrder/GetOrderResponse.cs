namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.GetOrder
{
    /// <summary>
    /// API response model for GetOrder operation
    /// </summary>
    public class GetOrderResponse
    {
        /// <summary>
        /// The unique identifier of the order
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The sequential order number
        /// </summary>
        public int OrderNumber { get; set; }

        /// <summary>
        /// The customer information for this order
        /// </summary>
        public GetOrderUserResponse Customer { get; set; }

        /// <summary>
        /// The branch information where the order was placed
        /// </summary>
        public GetOrderBranchResponse Branch { get; set; }

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
        public virtual List<GetOrderItemResponse> Items { get; set; } = []!;

        /// <summary>
        /// Flag to indicate if the order has been canceled
        /// </summary>
        public bool IsCanceled { get; set; }
    }
}
