namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateOrder
{
    /// <summary>
    /// API response model for branch information in order operations
    /// </summary>
    public class CreateOrderBranchResponse
    {
        /// <summary>
        /// The branch name
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The physical address of the branch
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// The postal/zip code of the branch location
        /// </summary>
        public string ZipCode { get; set; } = string.Empty;

        /// <summary>
        /// The city where the branch is located
        /// </summary>
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// The state or province where the branch is located
        /// </summary>
        public string State { get; set; } = string.Empty;
    }
}
