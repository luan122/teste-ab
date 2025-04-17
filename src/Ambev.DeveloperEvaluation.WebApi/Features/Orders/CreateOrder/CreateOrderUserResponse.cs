namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateOrder
{
    /// <summary>
    /// API response model for customer information in order operations
    /// </summary>
    public class CreateOrderUserResponse
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
