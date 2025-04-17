namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.GetOrder
{
    /// <summary>
    /// API response model for customer information in order operations
    /// </summary>
    public class GetOrderUserResponse
    {
        /// <summary>
        /// The customer's full name
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The customer's email address
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// The customer's phone number
        /// </summary>
        public string Phone { get; set; } = string.Empty;
    }
}
