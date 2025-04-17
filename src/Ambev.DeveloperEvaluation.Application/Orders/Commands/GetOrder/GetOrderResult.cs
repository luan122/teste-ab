using Ambev.DeveloperEvaluation.Application.Users.GetUser;

namespace Ambev.DeveloperEvaluation.Application.Orders.Commands.GetOrder;

/// <summary>
/// Response model for GetCategory operation
/// </summary>
public class GetOrderResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the newly created order.
    /// </summary>
    /// <value>A GUID that uniquely identifies the created order in the system.</value>
    public Guid Id { get; set; }
    public int OrderNumber { get; set; }
    public GetOrderUserResult Customer { get; set; }
    public GetOrderBranchResult Branch { get; set; }
    public decimal TotalOrderPrice { get; set; }
    public decimal TotalOrderPriceWithDiscount { get; set; }
    public virtual List<GetOrderItemResult> Items { get; set; } = []!;
}
