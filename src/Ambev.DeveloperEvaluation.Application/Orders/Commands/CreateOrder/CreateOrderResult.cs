﻿using Ambev.DeveloperEvaluation.Application.Users.GetUser;

namespace Ambev.DeveloperEvaluation.Application.Orders.Commands.CreateOrder;

/// <summary>
/// Represents the response returned after successfully creating a new order.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the newly created order,
/// which can be used for subsequent operations or reference.
/// </remarks>
public class CreateOrderResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the newly created order.
    /// </summary>
    /// <value>A GUID that uniquely identifies the created order in the system.</value>
    public Guid Id { get; set; }
    public int OrderNumber { get; set; }
    public CreateOrderUserResult Customer { get; set; }
    public CreateOrderBranchResult Branch { get; set; }
    public decimal TotalOrderPrice { get; set; }
    public decimal TotalOrderPriceWithDiscount { get; set; }
    public virtual List<CreateOrderItemResult> Items { get; set; } = []!;
    public DateOnly CreatedAt { get; set; }
}
