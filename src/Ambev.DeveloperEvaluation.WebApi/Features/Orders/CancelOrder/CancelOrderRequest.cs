namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.CancelOrder
{
    /// <summary>
    /// Request model for getting a order by ID
    /// </summary>
    public class CancelOrderRequest
    {
        /// <summary>
        /// The unique identifier of the order to retrieve
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// The order number of the order to retrieve
        /// </summary>
        public int OrderNumber { get; set; }
    }
}
