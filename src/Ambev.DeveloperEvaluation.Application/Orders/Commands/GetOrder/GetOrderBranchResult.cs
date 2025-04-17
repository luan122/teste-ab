namespace Ambev.DeveloperEvaluation.Application.Orders.Commands.GetOrder
{
    public class GetOrderBranchResult
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
    }
}